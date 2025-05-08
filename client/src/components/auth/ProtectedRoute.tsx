import AccountAPI from '../../network/AccountAPI';
import { auth } from '../../firebaseSetup';
import { initToken } from '../../utilities/api';
import { onAuthStateChanged } from 'firebase/auth';
import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';

export default function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(auth, async (user) => {
      if (user) {
        const token = await user.getIdToken();
        await initToken(token);
        const data = {
          accountId: user.uid,
          email: user.email || '',
          displayName: user.displayName || '',
        };
        await AccountAPI.createAccount({
          data: data,
        });
        setIsAuthenticated(true);
      } else {
        await initToken();
        setIsAuthenticated(false);
      }
      setIsLoading(false);
    });

    return () => unsubscribe();
  }, []);

  if (isLoading) return null;

  return isAuthenticated && !isLoading ? children : <Navigate to="/login" />;
}
