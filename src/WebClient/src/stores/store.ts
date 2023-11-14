import { createContext, useContext } from 'react';
import ModalStore from './modalStore';
import TokenStore from './tokenStore';
import UserStore from './userStore';
import DialogStore from './dialogStore';
import QuizStore from './quizStore';
import SessionStore from './sessionStore';

interface Store {
    modalStore: ModalStore;
    commonStore: TokenStore;
    userStore: UserStore;
    dialogStore: DialogStore;
    quizStore: QuizStore;
    sessionStore: SessionStore;
}

export const store: Store = {
    modalStore: new ModalStore(),
    commonStore: new TokenStore(),
    userStore: new UserStore(),
    dialogStore: new DialogStore(),
    quizStore: new QuizStore(),
    sessionStore: new SessionStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
