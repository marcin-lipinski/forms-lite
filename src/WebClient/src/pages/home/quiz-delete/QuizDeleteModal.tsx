import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store";
import './QuizDeleteModal.css';

interface Props {
    quizId: string
}

export default observer(function QuizDeleteModal({quizId}: Props) {
    const {modalStore, quizStore} = useStore()

    const handelQuizDeleteButton = () => {
        quizStore.deleteQuiz(quizId).then(() => handleCloseButtonClick()).catch(() => {});
    }

    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window quiz-delete">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <span>Do you want delete quiz and all related sessions?</span>
                <div id="buttons">
                    <button onClick={handleCloseButtonClick} className="white-button">Cancel</button>
                    <button onClick={handelQuizDeleteButton} className="orange-button">Confirm</button>
                </div>
            </div>
        </div>
    )
})