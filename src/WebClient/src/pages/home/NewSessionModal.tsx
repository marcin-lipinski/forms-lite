import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import './QuizDeleteModal.css';
import './QuizModal.css';
import { useState } from "react";
import { CreateSessionRequest } from "../../models/session";
import DatePicker from "react-datepicker";
import './NewSessionModal.css'

interface Props {
    quizId: string
}

export default observer(function NewSessionModal({quizId}: Props) {
    const {modalStore, sessionStore} = useStore()
    const [sessionSettings, setSessionSettings] = useState<CreateSessionRequest>({startTime: getDateNow(0), finishTime: getDateNow(1)});
    
    function getDateNow(offset: number){
        var date = new Date();
        date.setHours(date.getHours() + offset);
        var day = date.getDate().toString().padStart(2, "0");
        var month = (date.getMonth() + 1).toString().padStart(2, "0");;
        var year = date.getFullYear();
        var hour = date.getHours().toString().padStart(2, "0");;
        var minute = date.getMinutes().toString().padStart(2, "0");;
        
        return day + "-" + month + "-" + year + " " + hour + ':' + minute;
    }

    const makeDateFine = (evnt: React.FocusEvent<HTMLInputElement>) => {
        let elem = evnt.currentTarget;
        let date = elem.value;
        date = date.trim()
        date = date.padEnd(16, "0");

        var day = checkValidation(date.substring(0, 2));
        var month = checkValidation(date.substring(3, 5));
        var year = checkValidation(date.substring(6, 10));
        var hour = checkValidation(date.substring(11, 13));
        var minute = checkValidation(date.substring(14, 16));

        console.log(day + "&" + month + "&" + year + "&" + hour + "&" + minute);

        if(elem.id === "start-time"){
            sessionSettings.startTime = day + "-" + month + "-" + year + " " + hour + ':' + minute;
        } else {
            sessionSettings.finishTime = day + "-" + month + "-" + year + " " + hour + ':' + minute;
        }
        setSessionSettings(Object.assign({}, sessionSettings));
    }

    const checkValidation = (str: string) => {
        let allowed = '123456789';
        for (let i = 0; i < str.length; i++) {
            if(!allowed.includes(str.charAt(i)))
            {
                str = str.substring(0, i) + '0' + str.substring(i + 1);
            };
        }
        return str;
    }

    const handleInputChange = (evnt: React.ChangeEvent<HTMLInputElement>) => {
        const elem = evnt.currentTarget;
        if(elem.id === "start-time"){
            sessionSettings.startTime = elem.value;
        } else {
            sessionSettings.finishTime = elem.value;
        }
        setSessionSettings(Object.assign({}, sessionSettings));
    }

    const handelNewSessionButton = () => {
        console.log(sessionSettings)
        sessionStore.createSession(quizId, sessionSettings);
    }

    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body new-session">
                {sessionStore.partakeSessionUrl.length > 0
                    ?   <>
                            <p>Join session url:</p>
                            <p id="session-url">{sessionStore.partakeSessionUrl}</p>
                        </>
                    :   <>
                            <label htmlFor="start-time">Start time</label>
                            <input id="start-time" value={sessionSettings.startTime} onChange={handleInputChange} onBlur={makeDateFine}></input>
                            <label htmlFor="finish-time">Finish time</label>
                            <input id="finish-time" value={sessionSettings.finishTime} onChange={handleInputChange} onBlur={makeDateFine}></input>
                            <div id="buttons">
                                <button onClick={handleCloseButtonClick}>Cancel</button>
                                <button onClick={handelNewSessionButton}>Confirm</button>
                            </div>
                        </>
                }
                
            </div>
        </div>
    )
})