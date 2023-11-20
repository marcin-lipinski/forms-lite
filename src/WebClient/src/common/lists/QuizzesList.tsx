import { useEffect } from "react"
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import QuizModal from "../../pages/modals/quiz-view-modal/QuizModal";
import QuizDeleteModal from "../../pages/modals/quiz-delete-modal/QuizDeleteModal";
import NewSessionModal from "../../pages/modals/session-new-modal/NewSessionModal";
import { Quiz } from "../../models/quiz";

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

    const tableData = (quiz: Quiz) => [
        {title: "Title", value: quiz.title},
        {title: "Questions amount", value: quiz.questions.length},
        {title: "Id", value: quiz.id}
    ]

    return (
        <>
            {quizStore.allQuizzes.map((quiz, index) => 
                <div className="cover" key={quiz.id}>
                    <div className="table">
                        {tableData(quiz).map(res => 
                            <div className="table-row">
                                <p>{res.title}</p>
                                <p>{res.value}</p>
                            </div>
                        )}                        
                    </div>
                    <div className="button-group">
                        <button onClick={() => handleQuizSeeButton(index)} className="white-button">See</button>
                        <button onClick={() => handleNewSessionButton(quiz.id!)} className="white-button">Session</button>
                        <button onClick={() => handleQuizDeleteButton(quiz.id!)} className="orange-button">Delete</button>
                    </div>
                </div>
            )}
            <div className="cover add" onClick={hadleNewElementClick}>
                <div id="create"><button>+</button></div>
                <div className="button-group"></div>
            </div>
        </>
    )
})