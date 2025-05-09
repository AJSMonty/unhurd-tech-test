import Card from '../../../components/Card';
import useAllPromoTasks from '../hooks/useAllPromoTasks';
import CreateTaskButton from './CreateTaskButton';
import PromoTaskList from './PromoTaskList';

const PromoTasksCard = () => {
  const { promoTasks, completedTasks, toDoTasks, inProgressTasks } = useAllPromoTasks();

  return (
    <Card className="mt20">
      <div className="d-flex jc-space-between mb20">
        <div>
          <h3>Promo Tasks</h3>
          <p className="text-faded small">
            Todo: {toDoTasks} | In Progress: {inProgressTasks} | Completed: {completedTasks}
          </p>
        </div>
        <CreateTaskButton />
      </div>
      <PromoTaskList promoTasks={promoTasks} />
    </Card>
  );
};
export default PromoTasksCard;
