import { useEffect } from "react";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import { Session } from "../../models/session";
import SessionFinishModal from "./session-finish/SessionFinishModal";
import SessionDeleteModal from "./session-delete/SessionDeleteModal";
import SessionResult from "./session-result/SessionResult";
import './QuizzesList.css';

export default observer(function SessionsList() {
    const {sessionStore, modalStore} = useStore();
    const tableData = (session: Session) => [
        {title: "Quiz title", value: session.quizTitle},
        {title: "Active", value: session.isActive ? "YES" : "NO"},
        {title: "Planned start time", value: session.startTime.toString()},
        {title: "Planned finish time", value: session.finishTime.toString()},
        {title: "Answers amount", value: session.answers.length},
        {title: "Join url", value: session.joinUrl},
    ]

    useEffect(() => {
        sessionStore.getAll().catch(() => {});
    }, []);

    const handleSeeButtonClick = (session: Session) => {
        modalStore.openModal(<SessionResult session={session}/>)
    }

    const handleFinishButtonClick = (id: string) => {
        modalStore.openModal(<SessionFinishModal sessionId={id}/>)
    }
    const handleDeleteButtonClick = (id: string) => {
        modalStore.openModal(<SessionDeleteModal sessionId={id}/>)
    }

    return (
        <>
            {sessionStore.allSessions.map(session => 
                <div className="cover" key={session.id}>
                    <div className="table">
                        {tableData(session).map(res => 
                            <div className="table-row">
                                <p>{res.title}</p>
                                <p>{res.value}</p>
                            </div>
                        )}                        
                    </div>
                    <div className="button-group">
                        <button className="white-button" style={{cursor: !session.isActive ? "pointer" : "auto"}} disabled={session.isActive} onClick={() => handleSeeButtonClick(session)}>See</button>
                        <button className="white-button" style={{cursor: session.isActive ? "pointer" : "auto"}} disabled={!session.isActive} onClick={() => handleFinishButtonClick(session.id)}>Finish</button>
                        <button className="orange-button" onClick={() => handleDeleteButtonClick(session.id)}>Delete</button>
                    </div>
                </div>
            )}
        </>
    )
})