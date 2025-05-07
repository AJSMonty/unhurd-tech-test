import PromoTaskAPI from '../network/PromoTasksAPI';
import AccountAPI from '../network/AccountAPI';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { PromoTask } from '@/models/PromoTaskModel';
import { AccountModel } from '@/models/AccountModel';
import { auth } from '../firebaseSetup';
import { onAuthStateChanged } from 'firebase/auth';
import { initToken } from '../utilities/api';

const HomePage = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [promoTasks, setPromoTasks] = useState<PromoTask[]>([]);
  const [account, setAccount] = useState<AccountModel>();

  const goToLoginPage = () => {
    navigate('/login');
  };

  onAuthStateChanged(auth, (user) => {
    if (user) {
      console.log('Logged in:', user.email);
      user.getIdToken().then(async (token) => {
        await initToken(token);
        getDetails();
      });
    } else {
      console.log('Not authenticated');
    }
  });

  const getDetails = async () => {
    if (promoTasks.length > 0) return;
    AccountAPI.getAccount({ accountId: '12345' })
      .then((response) => {
        console.log('Account data:', response);
        setAccount(response.data);
        PromoTaskAPI.getAllPromoTasksFromAccountId({ accountId: '12345' })
          .then((promoTasksResponse) => {
            console.log('Promo tasks data:', promoTasksResponse);
            setPromoTasks(promoTasksResponse.data.tasks);
            setIsLoading(false);
          })
          .catch((error) => {
            console.error('Error fetching promo tasks data:', error);
            setIsLoading(false);
          });
      })
      .catch((error) => {
        console.error('Error fetching account data:', error);
        setIsLoading(false);
      });
  };

  return (
    <div>
      {isLoading && <p>Loading...</p>}
      {!isLoading && (
        <>
          <h1>Hi {account?.displayName}</h1>
          <h2>Promo Tasks</h2>
          {promoTasks?.map((task) => (
            <div key={task.id}>
              <h3>{task.title}</h3>
              <p>{task.description}</p>
            </div>
          ))}
        </>
      )}
      <button onClick={goToLoginPage}>Login</button>
    </div>
  );
};

export default HomePage;
