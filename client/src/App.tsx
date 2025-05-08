import './App.scss';
import AppRouter from './routing/AppRouter';
import { initApi } from './utilities/api';

initApi(import.meta.env.VITE_API_URL as string);

function App() {
  return <AppRouter />;
}

export default App;
