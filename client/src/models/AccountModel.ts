export type AccountModel = {
  id: string;
  accountId: string;
  email: string;
  displayName: string;
  createdAt: string;
};

export type CreateAccountModel = {
  accountId: string;
  email: string;
  displayName: string;
};
