import { makeAutoObservable, runInAction } from "mobx";
import { IUser, IUserLoginValues, IUserPasswordChange, IUserRegisterValues, IUserUsernameChange } from "../models/user";
import { store } from "./store";
import agents from "../api/agent";

export default class UserStore {
    user: IUser | null = null;
    loading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    register = async (creds: IUserRegisterValues) => {
        try {
            this.loading = true;
            const user = await agents.Account.register(creds);
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

    login = async (creds: IUserLoginValues) => {
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

    logout = async () => {
        store.commonStore.setToken(null);
        this.user = null;
    };

    current = async () => {
        try {
            this.loading = true;
            const user = await agents.Account.current();
            runInAction(() => {
                store.commonStore.setToken(user.token);
                this.user = user;
                this.loading = false;
            });
        }catch(error) {
            this.loading = false;
            throw new Error((error as Error).message);
        }
    }

    changeProfileImage = async (file: Blob) => {
        this.loading = true;
        try {
            let form = new FormData();
            form.append('File', file);
            form.append('FileType', file.type);
            const response = await agents.Account.uploadPhoto(form);
            runInAction(() => {
                this.loading = false;
                this.user!.profileImageUrl = response.data;
            });
        }
        catch(error){
            console.log(error);
            runInAction(() => this.loading = false);
        }
    }

    changePassword = async (values: IUserPasswordChange) => {
        this.loading = true;
        try {
            await agents.Account.changePassword(values);
            runInAction(() => {
                this.loading = false;
            });
        }
        catch(error){
            console.log(error);
            runInAction(() => this.loading = false);
        }
    }

    changeUsername = async (values: IUserUsernameChange) => {
        this.loading = true;
        try {
            await agents.Account.changeUsername(values);
            runInAction(() => {
                this.loading = false;
                this.user!.username = values.newUsername;
            });
        }
        catch(error){
            console.log(error);
            runInAction(() => this.loading = false);
        }
    }
}