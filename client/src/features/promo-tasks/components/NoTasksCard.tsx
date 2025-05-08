import Card from '../../../components/Card';
import CreateTaskButton from './CreateTaskButton';

const NoTasksCard = () => {
  return (
    <Card className="mt20 d-flex jc-space-between">
      <h3>No tasks yet</h3>
      <CreateTaskButton />
    </Card>
  );
};

export default NoTasksCard;
