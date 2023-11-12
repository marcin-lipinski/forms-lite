import { Route, Routes, useLocation, useNavigate } from 'react-router-dom';
import {AnimatePresence} from 'framer-motion';
import { useStore } from '../stores/store';
import { useEffect } from 'react';
import LoginPage from '../pages/login/LoginPage';
import HomePage from '../pages/home/HomePage';

export default function AppRoutes(){
    const {userStore, commonStore} = useStore();
    const location = useLocation();
    const navigate = useNavigate();

    // useEffect(() => {
    //     let pathname = location.pathname === '/' ? '/home' : location.pathname;
    //     if(pathname === '/session/join') {
    //         userStore.current()
    //             .then(() => navigate(pathname))
    //             .catch((error) => navigate(pathname))
    //             .finally(() => commonStore.setAppLoaded());
    //     }
    //     if (commonStore.token) {
    //         userStore.current()
    //             .then(() => navigate(pathname))
    //             .catch((error) => navigate('/'))
    //             .finally(() => commonStore.setAppLoaded());
    //     }
    //     else {
    //         commonStore.setAppLoaded();
    //         navigate('/');
    //     }
    // }, [commonStore, useStore]);

    //if(commonStore.loading) return <Dimmer active={true} style={{backgroundColor: 'rgba(0,0,0,0.4)'}}><Loader/></Dimmer>;
    
    return(
        <AnimatePresence mode='wait'>
            <Routes location={location} key={location.pathname}>
                <Route path='/' element={<LoginPage/>}/>
                <Route path='/home' element={<HomePage/>}/>
                {/* <Route path='/create' element={<NewQuizPage/>}/>
                <Route path='/session/new' element={<NewSessionPage/>}/>
                <Route path='*' element={<NotFoundPage/>}/> */}
            </Routes>
        </AnimatePresence>
    )
}