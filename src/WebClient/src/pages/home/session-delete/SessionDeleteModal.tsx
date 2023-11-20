import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store";
import './../quiz-delete/QuizDeleteModal.css';

interface Props {
    sessionId: string
}

export default observer(function SessionDeleteModal({sessionId}: Props) {
    const {modalStore, sessionStore} = useStore()

    const handelQuizDeleteButton = () => {
        sessionStore.deleteSession(sessionId).then(() => handleCloseButtonClick()).catch(() => {});
    }

    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window quiz-delete">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <span>Do you want to delete this session and al related answers?</span>
                <div id="buttons">
                    <button onClick={handleCloseButtonClick} className="white-button">Cancel</button>
                    <button onClick={handelQuizDeleteButton} className="orange-button">Confirm</button>
                </div>
            </div>
        </div>
    )
})