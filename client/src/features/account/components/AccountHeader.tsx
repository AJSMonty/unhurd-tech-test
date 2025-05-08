import { signOut } from 'firebase/auth';
import Card from '../../../components/Card';
import useAccount from '../hooks/useAccount';
import { Button } from '@mui/material';
import { auth } from '../../../firebaseSetup';

const AccountHeader = () => {
  const { account } = useAccount();

  const logout = async () => {
    await signOut(auth);
  };

  return (
    <Card>
      <div className="d-flex jc-space-between">
        <h3>Hi {account?.displayName}</h3>
        <Button className="btn-white" onClick={logout}>
          Logout
        </Button>
      </div>
    </Card>
  );
};

export default AccountHeader;
