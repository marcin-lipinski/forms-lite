import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import './Modal.css';

export default observer(function Modal() {
    const {modalStore} = useStore();

    return(
        <div id="modal" className={modalStore.modal.visible ? "visible" : ""}>
            {modalStore.modal.body}
        </div>
    )
})