import { onAuthStateChanged } from 'firebase/auth';
import './App.scss';
import { auth } from './firebaseSetup';
import AppRouter from './routing/AppRouter';
import { initApi, initToken } from './utilities/api';

initApi(import.meta.env.VITE_API_URL as string);

onAuthStateChanged(auth, (user) => {
  if (user) {
    console.log('Logged in:', user.email);
    user.getIdToken().then((token) => {
      initToken(token);
    });
  } else {
    console.log('Not authenticated');
  }
});

function App() {
  return <AppRouter />;
}

export default App;
