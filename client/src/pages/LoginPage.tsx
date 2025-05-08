import { initToken } from '../utilities/api';
import { auth } from '../firebaseSetup';
import { Button, CircularProgress, Icon } from '@mui/material';
import { FirebaseError } from 'firebase/app';
import { GoogleAuthProvider, signInWithPopup, User, UserCredential } from 'firebase/auth';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const LoginPage = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const googleProvider = new GoogleAuthProvider();

  const handleToken = async (token: string) => {
    try {
      await initToken(token);
      navigate('/');
    } catch (error) {
      console.error('Error initializing token:', error);
    }
  };

  const handleSignInSuccess = async (result: UserCredential) => {
    const user: User = result.user;
    if (user) {
      user.getIdToken().then((token) => {
        handleToken(token);
      });
      navigate('/');
      setIsLoading(false);
    }
  };

  const handleSignInError = (error: FirebaseError) => {
    processError(error);
    setIsLoading(false);
  };

  const processError = (error: FirebaseError) => {
    const code = error.code?.replace('auth/', '');
    console.log(code);
  };

  const signInWithGoogle = () => {
    setIsLoading(true);
    signInWithPopup(auth, googleProvider)
      .then(async (resp) => {
        await handleSignInSuccess(resp);
      })
      .catch((err) => {
        handleSignInError(err);
      });
  };

  return (
    <div className="login-page">
      <div className="container">
        <div className="login-header">
          <img src="/unhurd-logo.png" alt="logo"></img>
          <p className="mt16">un:hurd music Promo Task Manager</p>
        </div>
        <div className="form-container ml-auto mr-auto">
          <Button className="btn-white w100p mt16" disabled={isLoading} onClick={signInWithGoogle}>
            {!isLoading && (
              <>
                <Icon className="pr8">
                  <img src="/google-logo.svg" alt="" />
                </Icon>
                Continue with Google
              </>
            )}
            {isLoading && <CircularProgress size={16} />}
          </Button>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
