import useAccount from '../../../features/account/hooks/useAccount';
import PromoTaskAPI from '../../../network/PromoTasksAPI';
import { AxiosResponse } from 'axios';
import useSWRImmutable from 'swr/immutable';

const useAllPromoTasks = () => {
  const { account } = useAccount();
  const { data, isLoading, mutate, error } = useSWRImmutable<AxiosResponse | null>(
    () => {
      return `promo-tasks-data-${account?.accountId}`;
    },
    async () => {
      return await PromoTaskAPI.getAllPromoTasksFromAccountId({ accountId: account?.accountId });
    }
  );

  return {
    promoTasks: data?.data.tasks,
    promoTasksIsLoading: isLoading,
    promoTasksError: error,
    refetchPromoTasks: mutate,
  };
};

export default useAllPromoTasks;
