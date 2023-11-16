import { useEffect } from "react";
import { useStore } from "../../stores/store";
import { observer } from "mobx-react-lite";
import './QuizzesList.css';

export default observer(function SessionsList() {
    const {sessionStore} = useStore();

    useEffect(() => {
        sessionStore.getAll().catch(() => {});
    }, []);

    return (
        <>
            {sessionStore.allSessions.map(session => 
                <div className="cover">
                    <table>
                        <tr>
                            <td>Quiz title</td>
                            <td>{session.quizTitle}</td>
                        </tr>
                        <tr>
                            <td>Active</td>
                            <td>{session.isActive ? "YES" : "NO"}</td>
                        </tr>
                        <tr>
                            <td>Planned start time</td>
                            <td>{session.startTime.toString()}</td>
                        </tr>
                        <tr>
                            <td>Planned finish time</td>
                            <td>{session.finishTime.toString()}</td>
                        </tr>
                        <tr>
                            <td>Answers amount</td>
                            <td>{session.answersAmount}</td>
                        </tr>
                        <tr>
                            <td>Join url</td>
                            <td>{session.id}</td>
                        </tr>
                    </table>
                    <div className="button-group">
                        <button disabled={session.isActive}>See</button>
                        <button disabled={!session.isActive}>Finish</button>
                        <button>Delete</button>
                    </div>
                </div>
            )}
        </>
    )
})