import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import './QuizDeleteModal.css';
import './QuizModal.css';

interface Props {
    quizId: string
}

export default observer(function QuizDeleteModal({quizId}: Props) {
    const {modalStore, quizStore} = useStore()

    const handelQuizDeleteButton = () => {
        quizStore.deleteQuiz(quizId).then(() => handleCloseButtonClick());
    }

    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body close">
                <span>Do you want delete quiz and all related sessions?</span>
                <div id="buttons">
                    <button onClick={handleCloseButtonClick}>Cancel</button>
                    <button onClick={handelQuizDeleteButton}>Confirm</button>
                </div>
            </div>
        </div>
    )
})