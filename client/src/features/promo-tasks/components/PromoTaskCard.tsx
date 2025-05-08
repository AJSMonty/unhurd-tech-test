import PromoTaskAPI from '../../../network/PromoTasksAPI';
import Card from '../../../components/Card';
import { PromoTask, PromoTaskStatus } from '../../../models/PromoTaskModel';
import { MenuItem, Select } from '@mui/material';
import { useState } from 'react';
import useAllPromoTasks from '../hooks/useAllPromoTasks';

const PromoTaskCard = ({ promoTask }: { promoTask: PromoTask }) => {
  const { refetchPromoTasks } = useAllPromoTasks();
  const [isUpdating, setIsUpdating] = useState<boolean>(false);

  const updateTaskStatus = async (status: PromoTaskStatus) => {
    setIsUpdating(true);
    await PromoTaskAPI.updatePromoTask({
      taskId: promoTask.id,
      data: { ...promoTask, status },
    });
    await refetchPromoTasks();
    setIsUpdating(false);
  };

  const getStatusClass = (status: PromoTaskStatus) => {
    switch (status) {
      case 'ToDo':
        return 'to-do';
      case 'InProgress':
        return 'in-progress';
      case 'Done':
        return 'done';
      default:
        return '';
    }
  };

  return (
    <Card className={`d-flex ${getStatusClass(promoTask.status)}`}>
      <div>
        <h3>{promoTask.title}</h3>
        <p className="mt10">{promoTask.description}</p>
        <p className="text-faded small mt10">Date created: {new Date(promoTask.createdAt).toLocaleDateString()}</p>
      </div>
      <Select
        value={promoTask.status}
        disabled={isUpdating}
        onChange={(e) => updateTaskStatus(e.target.value)}
        className="ml-auto max-w200 max-h40"
      >
        <MenuItem value="ToDo">To Do</MenuItem>
        <MenuItem value="InProgress">In Progress</MenuItem>
        <MenuItem value="Done">Done</MenuItem>
      </Select>
    </Card>
  );
};

export default PromoTaskCard;
