import LoginPage from "../pages/LoginPage";
import HomePage from "../pages/HomePage";

export type AppRoute = {
  path: string;
  element: React.FC;
  protected?: boolean;
};

export const appRoutes: AppRoute[] = [
  { path: "/", element: HomePage },
  { path: "/login", element: LoginPage },
];
