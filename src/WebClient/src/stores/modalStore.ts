import { makeAutoObservable } from 'mobx';

interface Modal {
    visible: boolean;
    body: JSX.Element | null;
}

export default class ModalStore {
    modal: Modal = {
        visible: false,
        body: null,
    };

    constructor() {
        makeAutoObservable(this);
    }

    openModal = (content: JSX.Element) => {
        this.modal.visible = true;
        this.modal.body = content;
    };

    closeModal = () => {
        this.modal.visible = false;
        this.modal.body = null;
    };
}