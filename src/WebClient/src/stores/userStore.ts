import { makeAutoObservable, runInAction } from "mobx";
import { store } from "./store";
import agents from "../api/agent";
import { User, UserLoginRequest, UserRegisterRequest } from "../models/user/user";

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
        try {
            this.loading = true;
            const user = await agents.Account.register(data);
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
                this.loading = false;
            });
        } catch (error) {
            this.loading = false;
        }
    };

    login = async (creds: UserLoginRequest) => {
        try {
            this.loading = true;
            const user = await agents.Account.login(creds);
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
                this.loading = false;
            });
        } catch (error) {
            this.loading = false;
            throw new Error((error as Error).message);
        }
    };

    // logout = async () => {
    //     store.commonStore.setToken(null);
    //     this.user = null;
    // };

    // current = async () => {
    //     try {
    //         this.loading = true;
    //         const user = await agents.Account.current();
    //         runInAction(() => {
    //             store.commonStore.setToken(user.token);
    //             this.user = user;
    //             this.loading = false;
    //         });
    //     }catch(error) {
    //         this.loading = false;
    //         throw new Error((error as Error).message);
    //     }
    // }
}