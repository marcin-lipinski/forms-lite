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
                {session.answers.length === 0
                    ? <span style={{color: "white", fontSize: "1.2rem", padding: "10px"}}>No results</span>
                    : <div className="result-table-div">
                            <table className="result-table">
                                <thead>
                                    <tr>
                                        <th className="result-table-cell"></th>
                                        {session.answers.map((answer, index) => 
                                            <th key={index} className="result-table-cell">{answer.participantName}</th>
                                        )}
                                    </tr>
                                </thead>
                                <tbody>                            
                                    {session.questions.map((question, index) => 
                                        <tr key={index}>
                                            <td className="result-table-cell">{question.contentText}</td>
                                            {session.answers.map((answer, index2) => 
                                                <td key={index * 10 + index2} className="result-table-cell">{answer.answers.find(a => a.id === question.id)?.questionAnswer}</td>
                                            )}
                                        </tr>
                                    )}            
                                </tbody>
                            </table>
                      </div>
                }
            </div>
        </div>
    )
}