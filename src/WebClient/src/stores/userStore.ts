import { makeAutoObservable, runInAction } from "mobx";
import { store } from "./store";
import agents from "../api/agent";
import { User, UserRegisterRequest, UserLoginRequest } from "../models/user";
import axios from "axios";

export default class UserStore {
    user: User | null = null;
    loading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    register = async (data: UserRegisterRequest) => {
        console.log(axios.defaults.baseURL)
        try {
            runInAction(() => this.loading = true);
            const user = await agents.Account.register(data);
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
            });
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    login = async (creds: UserLoginRequest) => {
        try {
            runInAction(() => this.loading = true);
            const user = await agents.Account.login(creds);
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
            });
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    logout = async () => {
        store.commonStore.setToken(null);
        this.user = null;
    };

    current = async () => {
        try {
            runInAction(() => this.loading = true);
            const user = await agents.Account.current();
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
            });
        }catch(error) {
            this.loading = false;
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }
}