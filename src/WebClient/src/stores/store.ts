import { createContext, useContext } from 'react';
import ModalStore from './modalStore';
import CommonStore from './commonStore';
import UserStore from './userStore';
import QuizStore from './quizStore';
import QuizBrowserStore from './quizBrowserStore';
import QuizDetailsStore from './quizDetailsStore';
import QuizUserStore from './quizUserStore';
import { NewSessionStore } from './newSessionStore';
import { LiveSessionStore } from './liveSessionStore';
import DialogWindowStore from './dialogWindowStore';

interface Store {
    modalStore: ModalStore;
    commonStore: CommonStore;
    userStore: UserStore;
    warningFooterStore: DialogWindowStore;
    quizStore: QuizStore;
    quizBrowserStore: QuizBrowserStore;
    quizDetailsStore: QuizDetailsStore;
    quizUserStore: QuizUserStore;
    newSessionStore: NewSessionStore;
    liveSessionStore: LiveSessionStore;
}

export const store: Store = {
    modalStore: new ModalStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    warningFooterStore: new DialogWindowStore(),
    quizStore: new QuizStore(),
    quizBrowserStore: new QuizBrowserStore(),
    quizDetailsStore: new QuizDetailsStore(),
    quizUserStore: new QuizUserStore(),
    newSessionStore: new NewSessionStore(),
    liveSessionStore: new LiveSessionStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
