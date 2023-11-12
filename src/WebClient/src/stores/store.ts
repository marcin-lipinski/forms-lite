import { createContext, useContext } from 'react';
import ModalStore from './modalStore';
import TokenStore from './tokenStore';
import UserStore from './userStore';
import DialogStore from './dialogStore';

interface Store {
    modalStore: ModalStore;
    commonStore: TokenStore;
    userStore: UserStore;
    dialogStore: DialogStore;
}

export const store: Store = {
    modalStore: new ModalStore(),
    commonStore: new TokenStore(),
    userStore: new UserStore(),
    dialogStore: new DialogStore()
};

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}
