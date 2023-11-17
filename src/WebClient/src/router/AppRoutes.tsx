import { Route, Routes, useLocation, useNavigate } from 'react-router-dom';
import {AnimatePresence} from 'framer-motion';
import { useStore } from '../stores/store';
import { useEffect } from 'react';
import LoginPage from '../pages/login/LoginPage';
import HomePage from '../pages/home/HomePage';
import NotFoundPage from '../pages/not-found/NotFoundPage';
import SessionPage from '../pages/session/SessionPage';
import Laoder from '../common/loader/Loader';
import { observer } from 'mobx-react-lite';

export default observer(function AppRoutes(){
    const {userStore, commonStore} = useStore();
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        if(location.pathname.startsWith('/partake')) {
            commonStore.setAppLoaded();
            navigate(location.pathname);
        }
        else if(commonStore.token) {
            userStore.current()
                .then(() => navigate('/home'))
                .catch(() => navigate('/'))
                .finally(() => commonStore.setAppLoaded());
        }
        else {
            commonStore.setAppLoaded();
            navigate('/');
        }
    }, []);

    <Laoder active={commonStore.loading}/>;
    
    return(
        <AnimatePresence mode='wait'>
            <Routes location={location} key={location.pathname}>
                <Route path='/' element={<LoginPage/>}/>
                <Route path='/home' element={<HomePage/>}/>
                <Route path='/partake/:id' element={<SessionPage/>}/>
                <Route path='*' element={<NotFoundPage/>}/>
            </Routes>
        </AnimatePresence>
    )
})