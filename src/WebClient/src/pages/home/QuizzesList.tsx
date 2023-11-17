import { useEffect } from "react"
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import QuizModal from "./quiz-view/QuizModal";
import QuizDeleteModal from "./quiz-delete/QuizDeleteModal";
import NewSessionModal from "./session-new/NewSessionModal";

export default observer(function QuizzesList() {
    const {quizStore, modalStore} = useStore();

    useEffect(() => {
        quizStore.getAll().catch(() => {});
    }, []);

    const handleQuizSeeButton = (index: number) => {
        modalStore.openModal(<QuizModal quiz={quizStore.allQuizzes[index]} mode={"view"}/>)
    }

    const handleNewSessionButton = (id: string) => {
        modalStore.openModal(<NewSessionModal quizId={id} />);
    }

    const handleQuizDeleteButton = (id: string) => {
        modalStore.openModal(<QuizDeleteModal quizId={id}/>)
    }

    const hadleNewElementClick = () => {
        modalStore.openModal(<QuizModal mode="create"/>)
    }

    return (
        <>
            {quizStore.allQuizzes.map((quiz, index) => 
                <div className="cover" key={quiz.id}>
                    <table>
                        <tbody>
                            <tr>
                                <td>Title</td>
                                <td>{quiz.title}</td>
                            </tr>
                            <tr>
                                <td>Questions amount</td>
                                <td>{quiz.questions.length}</td>
                            </tr>
                            <tr>
                                <td>Id</td>
                                <td>{quiz.id}</td>
                            </tr>
                        </tbody>                        
                    </table>
                    <div className="button-group">
                        <button onClick={() => handleQuizSeeButton(index)} className="white-button">See</button>
                        <button onClick={() => handleNewSessionButton(quiz.id!)} className="white-button">Session</button>
                        <button onClick={() => handleQuizDeleteButton(quiz.id!)} className="orange-button">Delete</button>
                    </div>
                </div>
            )}
            <div className="cover add" onClick={hadleNewElementClick}>
                <div id="create"><button>+</button></div>
                <table>
                    <tbody>
                        <tr>
                            <td>s</td>
                            <td>s</td>
                        </tr>
                        <tr>
                            <td>s </td>
                            <td>s</td>
                        </tr>
                        <tr>
                            <td>s</td>
                            <td>s</td>
                        </tr>
                    </tbody>
                </table>
                <div className="button-group">
                </div>
            </div>
        </>
    )
})