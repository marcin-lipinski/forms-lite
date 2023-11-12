import { makeAutoObservable } from 'mobx';

export default class SessionStore {

    constructor() {
        makeAutoObservable(this);
    }


}