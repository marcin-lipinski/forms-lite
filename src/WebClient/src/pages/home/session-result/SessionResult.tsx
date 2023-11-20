import { useStore } from "../../../stores/store";

export default function SessionResult() {
    const {modalStore} = useStore();
    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window quiz-view">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                
            </div>
        </div>
    )
}