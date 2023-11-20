import { Question, QuestionType } from "../../models/quiz"

interface Props {
    question: Question;
    modeState: string;
    index: number;
    keyDownOnEditableHandler: (evnt: React.ChangeEvent<HTMLTextAreaElement>, index: number, answer: string) => void;
}

export default function QuestionAnswers({question, modeState, keyDownOnEditableHandler, index}: Props) {
    if(question.questionType === QuestionType.Open) {
        return(
            <div className="question-answers">
                <div id="open-answer">User answer</div>
            </div>
        )
    }

    return(
        <div className="question-answers closed">
            {[0, 1, 2, 3].map(answer =>
                <div className={`closed-answer ${modeState !== 'view' ? "editable" : ""}`}>
                    <textarea maxLength={40} disabled={modeState === 'view'} onChange={(evnt) => keyDownOnEditableHandler(evnt, index, answer.toString())} value={question.answers![answer]}/>
                </div>
            )}
        </div>
    )
}