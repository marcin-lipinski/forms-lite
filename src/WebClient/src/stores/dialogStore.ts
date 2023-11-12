import { makeAutoObservable } from 'mobx';

export default class DialogStore {
    message: string[] = [];
    error: boolean = false;
    visible: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    showError = (message: string[]) => {

        this.message = message;
        this.error = true;
        this.visible = true;
        setTimeout(this.closeDialog, 30000);
    }

    closeDialog = () => {
        this.visible = false;
    }
}