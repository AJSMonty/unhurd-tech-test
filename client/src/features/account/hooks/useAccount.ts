import { auth } from '../../../firebaseSetup';
import AccountAPI from '../../../network/AccountAPI';
import { AxiosResponse } from 'axios';
import useSWRImmutable from 'swr/immutable';

const useAccount = () => {
  const id = auth.currentUser?.uid;
  const { data, isLoading, mutate, error } = useSWRImmutable<AxiosResponse | null>(
    () => {
      return `account-data-${id}`;
    },
    async () => {
      return await AccountAPI.getAccount({ accountId: id });
    }
  );

  return {
    account: data?.data,
    accountIsLoading: isLoading,
    accountError: error,
    refetchAccount: mutate,
  };
};

export default useAccount;
