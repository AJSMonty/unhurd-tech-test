import Card from '../../../components/Card';
import { PromoTask } from '../../../models/PromoTaskModel';

const PromoTaskCard = ({ promoTask }: { promoTask: PromoTask }) => {
  return (
    <Card>
      <div>
        <h3>{promoTask.title}</h3>
        <p>{promoTask.description}</p>
        <p>Status: {promoTask.status}</p>
        <p>Created At: {new Date(promoTask.createdAt).toLocaleDateString()}</p>
      </div>
    </Card>
  );
};

export default PromoTaskCard;
