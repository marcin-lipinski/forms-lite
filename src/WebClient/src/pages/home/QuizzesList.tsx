import { useEffect } from "react"
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import QuizModal from "./QuizModal";
import './QuizzesList.css';
import QuizModalCreate from "./QuizModalCreate";

export default observer(function QuizzesList() {
    const {quizStore, modalStore} = useStore();

    useEffect(() => {
        quizStore.getAll()
    }, []);

    const handleQuizSeeButton = (id: string) => {
        modalStore.openModal(<QuizModal quizId={id}/>)
    }

    const hadleNewElementClick = () => {
        modalStore.openModal(<QuizModalCreate/>)
    }

    return (
        <>
            {quizStore.allQuizzes.map(quiz => 
                <div className="cover">
                    <table>
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
                    </table>
                    <div className="button-group">
                        <button onClick={() => handleQuizSeeButton(quiz.id!)}>See</button>
                        <button>Delete</button>
                    </div>
                </div>
            )}
            <div className="cover add" onClick={hadleNewElementClick}>
                <div id="create"><button>+</button></div>
                <table>
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
                </table>
                <div className="button-group">
                </div>
            </div>
        </>
    )
})