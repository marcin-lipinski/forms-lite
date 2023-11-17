import { BrowserRouter as Router } from 'react-router-dom';
import AppRoutes from './router/AppRoutes';
import Dialog from './common/dialog/Dialog';
import Modal from './common/modal/Modal';
import './App.css';

export default function App() {
  return (
    <Router >
      <AppRoutes/>
      <Dialog/>
      <Modal/>
    </Router>
  );
}
