import HomePage from "../pages/HomePage";

export type AppRoute = {
  path: string;
  element: React.FC;
  protected?: boolean;
};

export const appRoutes: AppRoute[] = [{ path: "/", element: HomePage }];
