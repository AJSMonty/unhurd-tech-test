import { Button } from '@mui/material';
import useAllPromoTasks from '../hooks/useAllPromoTasks';
import PromoTaskAPI from '../../../network/PromoTasksAPI';
import useAccount from '../../../features/account/hooks/useAccount';
import { CreatePromoTask } from '../../../models/PromoTaskModel';

const CreateTaskButton = () => {
  const { account } = useAccount();
  const { refetchPromoTasks } = useAllPromoTasks();

  const createTask = () => {
    const newTask: CreatePromoTask = {
      accountId: account?.accountId,
      title: 'New Task',
      description: 'This is a new task',
      status: 'ToDo',
    };
    PromoTaskAPI.createPromoTask({ data: newTask })
      .then((response) => {
        console.log('Task created successfully:', response);
        refetchPromoTasks();
      })
      .catch((error) => {
        console.error('Error creating task:', error);
      });
  };

  return (
    <>
      <Button className="btn-white" onClick={createTask}>
        Create task
      </Button>
    </>
  );
};

export default CreateTaskButton;
