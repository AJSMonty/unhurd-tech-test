import axios from "axios";

import { AccountModel, CreateAccountModel } from "@/models/AccountModel";

class AccountAPI {
  static getAccount = async ({ accountId }: { accountId?: string }) =>
    await axios.get<AccountModel>(`/account/${accountId}`);

  static createAccount = async ({ data }: { data: CreateAccountModel }) =>
    await axios.post<AccountModel>("/account", data);
}

export default AccountAPI;
