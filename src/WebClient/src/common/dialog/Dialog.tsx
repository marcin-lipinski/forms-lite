import { observer } from "mobx-react-lite"
import { useStore } from "../../stores/store"
import './Dialog.css';

export default observer(function Dialog(){
    const {dialogStore} = useStore();
    const {visible, message, error, closeDialog} = {...dialogStore};

    const handleCloseButtonClick = () => closeDialog();

    return(
        <div className={visible ? "dialog visible ": "dialog"}>
            <header>
                <p>{error ? "Error" : "Success"}</p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div>
                <ul>
                    {message.map((m, index) => 
                        <li key={index}>{m}</li>
                    )}
                </ul>
            </div>
        </div>
    )
})