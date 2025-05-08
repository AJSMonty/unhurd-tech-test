import Card from '../../../components/Card';
import useAllPromoTasks from '../hooks/useAllPromoTasks';
import CreateTaskButton from './CreateTaskButton';
import PromoTaskList from './PromoTaskList';

const PromoTasksCard = () => {
  const { promoTasks } = useAllPromoTasks();

  return (
    <Card className="mt20">
      <div className="d-flex jc-space-between mb20">
        <h3>Promo Tasks</h3>
        <CreateTaskButton />
      </div>
      <PromoTaskList promoTasks={promoTasks} />
    </Card>
  );
};
export default PromoTasksCard;
