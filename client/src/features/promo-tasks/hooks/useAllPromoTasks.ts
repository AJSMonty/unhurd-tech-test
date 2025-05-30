import { PromoTask } from '@/models/PromoTaskModel';
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
    completedTasks: data?.data.tasks.filter((task: PromoTask) => task.status === 'Done').length,
    inProgressTasks: data?.data.tasks.filter((task: PromoTask) => task.status === 'InProgress').length,
    toDoTasks: data?.data.tasks.filter((task: PromoTask) => task.status === 'ToDo').length,
    promoTasksCount: data?.data.tasks.length,
    promoTasksIsLoading: isLoading,
    promoTasksError: error,
    refetchPromoTasks: mutate,
  };
};

export default useAllPromoTasks;
