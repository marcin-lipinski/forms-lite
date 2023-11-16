import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useStore } from "../../stores/store";
import { QuestionType } from "../../models/quiz";
import { PartakeSessionFinishRequest } from "../../models/session";

export default observer(function SessionPage() {
    const {id} = useParams();
    const {sessionStore} = useStore();
    const navigate = useNavigate();    
    const [result, setResult] = useState<PartakeSessionFinishRequest>({participant: "", answers: []});
    
    useEffect(() => {
        if(id === undefined) navigate("/notfound");
        else sessionStore.partakeSession(id!).then(() => fillResult()).catch(() => navigate("/notfound"));
    }, [])

    const fillResult = () => {
        sessionStore.partakeQuiz?.questions.forEach(q => result.answers.push({questionNumber: q.questionNumber, questionAnswer: ""})); 
        setResult(Object.assign({}, result));
    }       

    const handleSendResult = () => {
        sessionStore.partakeSessionFinish(id!, result).catch(() => {});
    }

    if(sessionStore.loading) return <>Loading</>;

    return (
        <div className="app-page">
            <header>{sessionStore.partakeQuiz?.title}</header>
            {sessionStore.partakeQuiz?.questions.map(question => 
                <div className="question-whole">
                    <div className={"question-content"} style={question.image !== null ? {height: "360px"} : {}}>
                        <div className={"editable"}>
                            <textarea value={question.contentText}/>
                        </div>
                        {question.image === null 
                            ? <></>
                            : <img className="question-image" src={question.contentImageUrl}/>
                        }
                    </div>
                    <div className={question.questionType === QuestionType.Open ? "question-answers" : "question-answers closed"}>
                        {question.questionType === QuestionType.Open
                            ? <div id="open-answer">User answer</div>
                            : <>
                                <div className={"closed-answer editable"}>
                                    <textarea value={question.answers![0]}/>
                                </div>
                                <div className={"closed-answer editable"} >
                                    <textarea value={question.answers![1]}/>
                                </div>
                                <div className={"closed-answer editable"}>
                                    <textarea value={question.answers![2]}/>
                                </div>
                                <div className={"closed-answer editable"}>
                                    <textarea value={question.answers![3]}/>
                                </div>
                            </>
                        }
                    </div>
                    <button className="question-delete-button" onClick={handleSendResult}>🗑</button>                     
                </div>    
            )}
        </div>
    )
})