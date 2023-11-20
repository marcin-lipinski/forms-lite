import { Session } from "../../../models/session";
import { useStore } from "../../../stores/store";
import "./SessionResult.css";
import "../quiz-view-modal/QuizModal.css";

interface Props {
    session: Session
}

export default function SessionResult({session}: Props) {
    const {modalStore} = useStore();
    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window session-result">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <table className="result-table">
                    <thead>
                        <tr>
                            <th className="result-table-cell"></th>
                            {session.answers.map(answer => 
                                    <th className="result-table-cell">{answer.participantName}</th>
                            )}
                        </tr>
                    </thead>
                    <tbody>
                        
                        {session.questions.map((question) => 
                            <tr>
                                <td className="result-table-cell">{question.contentText}</td>
                                {session.answers.map(answer => 
                                    <>
                                        <td className="result-table-cell">{answer.answers.find(a => a.id === question.id)?.questionAnswer}</td>
                                    </>    
                                )}
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    )
}