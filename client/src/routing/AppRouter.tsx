// AppRouter.tsx
import { Routes, Route } from 'react-router-dom';
import { appRoutes } from './Routes';
import ProtectedRoute from '../components/auth/ProtectedRoute';

export default function AppRouter() {
  return (
    <Routes>
      {appRoutes.map(({ path, element: Component, protected: isProtected }) => (
        <Route
          key={path}
          path={path}
          element={isProtected ? <ProtectedRoute>{<Component />}</ProtectedRoute> : <Component />}
        />
      ))}
    </Routes>
  );
}
