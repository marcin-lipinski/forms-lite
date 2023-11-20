import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store";
import './../quiz-delete/QuizDeleteModal.css';

interface Props {
    sessionId: string
}

export default observer(function SessionFinishModal({sessionId}: Props) {
    const {modalStore, sessionStore} = useStore()

    const handelQuizDeleteButton = () => {
        sessionStore.finishSession(sessionId).then(() => handleCloseButtonClick()).catch(() => {});
    }

    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window quiz-delete">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <span>Do you want to finish this session?</span>
                <div id="buttons">
                    <button onClick={handleCloseButtonClick} className="white-button">Cancel</button>
                    <button onClick={handelQuizDeleteButton} className="orange-button">Confirm</button>
                </div>
            </div>
        </div>
    )
})