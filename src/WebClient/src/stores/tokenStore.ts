import { makeAutoObservable, reaction } from 'mobx';

export default class TokenStore {
    token: string | null = localStorage.getItem('jwt');
    loading = true;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token,
            (token) => {
                if (token) localStorage.setItem('jwt', token);
                else localStorage.removeItem('jwt');
            }
        );
    }

    setToken = (token: string | null) => {
        this.token = token;
    };

    setAppLoaded = () => {
        this.loading = false;
    };
}