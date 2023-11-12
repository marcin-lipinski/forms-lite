import { useEffect } from "react"
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import './QuizzesList.css';

export default observer(function QuizzesList() {
    const {quizStore} = useStore();

    useEffect(() => {
        quizStore.getAll()
    }, []);

    return (
        <>
            {quizStore.allQuizes.map(quiz => 
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
                        <button>See</button>
                        <button>Delete</button>
                    </div>
                </div>
            )}
        </>
    )
})