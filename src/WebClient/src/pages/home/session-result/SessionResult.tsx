import { Session } from "../../../models/session";
import { useStore } from "../../../stores/store";

interface Props {
    session: Session
}

export default function SessionResult({session}: Props) {
    const {modalStore} = useStore();
    const handleCloseButtonClick = () => modalStore.closeModal();

    return(
        <div className="modal-window quiz-view">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                
            </div>
        </div>
    )
}