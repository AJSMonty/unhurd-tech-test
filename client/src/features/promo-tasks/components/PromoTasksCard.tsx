import Card from '../../../components/Card';
import useAllPromoTasks from '../hooks/useAllPromoTasks';
import CreateTaskButton from './CreateTaskButton';
import PromoTaskList from './PromoTaskList';

const PromoTasksCard = () => {
  const { promoTasks } = useAllPromoTasks();

  return (
    <Card className="mt20">
      <div className="d-flex jc-space-between">
        <h4>Promo Tasks</h4>
        <CreateTaskButton />
      </div>
      <PromoTaskList promoTasks={promoTasks} />
    </Card>
  );
};
export default PromoTasksCard;
