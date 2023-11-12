import { BrowserRouter as Router } from 'react-router-dom';
import AppRoutes from './router/AppRoutes';
import Dialog from './common/dialog/Dialog';
import './App.css';

export default function App() {
  return (
    <Router >
      <AppRoutes></AppRoutes>
      <Dialog></Dialog>
    </Router>
  );
}
