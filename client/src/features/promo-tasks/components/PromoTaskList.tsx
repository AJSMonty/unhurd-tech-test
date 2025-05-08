import { PromoTask } from '../../../models/PromoTaskModel';
import PromoTaskCard from './PromoTaskCard';

const PromoTaskList = ({ promoTasks }: { promoTasks: PromoTask[] }) => {
  return (
    <div className="d-flex flex-d-col gap10">
      {promoTasks?.map((promoTask) => (
        <PromoTaskCard key={promoTask.id} promoTask={promoTask} />
      ))}
    </div>
  );
};

export default PromoTaskList;
