import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useStore } from "../../stores/store";
import { Question, QuestionType } from "../../models/quiz";
import { PartakeSessionFinishRequest } from "../../models/session";
import "./SessionPage.css";
import Laoder from "../../common/loader/Loader";

export default observer(function SessionPage() {
    const {id} = useParams();
    const {sessionStore} = useStore();
    const [result, setResult] = useState<PartakeSessionFinishRequest>({participant: "", answers: []});
    const [send, setSend] = useState<boolean | undefined>(undefined);
    const navigate = useNavigate();
    const handleGoHomeClick = () => navigate('/');
    
    useEffect(() => {
        if(id === undefined) navigate("/notfound");
        else sessionStore.partakeSession(id!).then(() => fillResult()).catch(() => navigate("/notfound"));
    }, [])

    const fillResult = () => {
        sessionStore.partakeQuiz?.questions.forEach(q => result.answers.push({id: q.id, questionAnswer: ""})); 
        setResult(Object.assign({}, result));
    }       

    const handleParticipantChange = (evnt: React.ChangeEvent<HTMLInputElement>) => {
        result.participant = evnt.currentTarget.value;
        setResult(Object.assign({}, result));
    }

    const handleSendButtonClick = () => {
        sessionStore.partakeSessionFinish(id!, result).then(() => setSend(true)).catch(() => {});
    }

    const handleSelectAnswer = (id: string, answer: number) => {
        result.answers.find(q => q.id === id)!.questionAnswer = answer.toString();
        setResult(Object.assign({}, result));
    }

    const handleTypeAnswer =(evnt: React.ChangeEvent<HTMLTextAreaElement>, id: string) => {
        result.answers.find(q => q.id === id)!.questionAnswer = evnt.currentTarget.value;
        setResult(Object.assign({}, result));
    }

    if(sessionStore.loading) return <Laoder/>

    if(send !== undefined) {
        return (
            <div className="app-page session">
                <div className={send ? "session-result success" : "session-result failed"}>
                    <header>{send ? "✓ Success" : "⚠ Failed"}</header>
                    <a onClick={handleGoHomeClick}>Go Home page</a>
                </div>
            </div>
        )
    }
    if(result.answers.length <= 0) return<></>
    return (
        <div className="app-page session">                    
            <div id="session-quiz">
                <p className='view'>
                    {sessionStore.partakeQuiz?.title}
                </p>
                {sessionStore.partakeQuiz?.questions.map((question, index) => 
                    <div className="question-whole" key={question.id}>
                        <div className={"question-content"} style={question.image !== null || question.contentImageUrl?.length !== 0 ? {height: "360px"} : {}}>
                            <div className="view">
                                {question.contentText}
                            </div>
                            {(question.contentImageUrl?.length !== 0 || question.image !== null)
                                ? <div className="question-image-contaner">
                                    <img className="question-image" src={question.contentImageUrl}/>
                                </div>
                                : <></>
                            }
                        </div>
                            {question.questionType === QuestionType.Open
                                ? <div className="question-answers">
                                    <textarea maxLength={250} className="open-answer" value={result.answers[index].questionAnswer} placeholder="User answer" onChange={(evnt) => handleTypeAnswer(evnt, question.id)}/>
                                  </div>
                                : <div className="question-answers closed">
                                    {[0, 1, 2, 3].map(answer =>
                                    <div key={answer} className={`closed-answer view ${result.answers[index].questionAnswer === answer.toString() ? "selected" : ""}`} onClick={() => handleSelectAnswer(question.id, answer)}>
                                        {question.answers![answer]}
                                    </div>
                                    )}
                                  </div>
                            }                                             
                    </div>    
                )}
                <div id="session-send">
                    <input placeholder="Your name" onChange={handleParticipantChange} value={result.participant}/>
                    <button className="orange-button" onClick={handleSendButtonClick}>Send</button>
                </div>
            </div>
        </div>
    )
})