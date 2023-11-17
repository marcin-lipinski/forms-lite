import ReactDOM from 'react-dom/client';
import App from './App';
import { StoreContext, store } from './stores/store';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <StoreContext.Provider value={store}>
      <App/>
  </StoreContext.Provider>
);

reportWebVitals();
