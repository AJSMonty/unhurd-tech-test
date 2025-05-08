import useAccount from '../features/account/hooks/useAccount';
import useAllPromoTasks from '../features/promo-tasks/hooks/useAllPromoTasks';
import AccountHeader from '../features/account/components/AccountHeader';
import NoTasksCard from '../features/promo-tasks/components/NoTasksCard';
import PromoTasksCard from '../features/promo-tasks/components/PromoTasksCard';

const HomePage = () => {
  const { accountIsLoading } = useAccount();
  const { promoTasks, promoTasksIsLoading, promoTasksError } = useAllPromoTasks();

  return (
    <div className="p20">
      {!accountIsLoading && <AccountHeader />}
      {!promoTasksIsLoading && promoTasks && <PromoTasksCard />}
      {!promoTasksIsLoading && promoTasksError && <NoTasksCard />}
    </div>
  );
};

export default HomePage;
