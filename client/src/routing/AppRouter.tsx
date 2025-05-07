// AppRouter.tsx
import { Routes, Route } from "react-router-dom";
import { appRoutes } from "./Routes";
// import ProtectedRoute from './components/ProtectedRoute';

export default function AppRouter() {
  return (
    <Routes>
      {appRoutes.map(({ path, element: Component }) => (
        <Route
          key={path}
          path={path}
          element={<Component />}
          //   element={isProtected ? <ProtectedRoute>{element}</ProtectedRoute> : element}
        />
      ))}
    </Routes>
  );
}
