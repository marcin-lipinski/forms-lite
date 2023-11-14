import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useStore } from "../../stores/store";
import './QuizModal.css';
import { QuestionType, Quiz, UpdateQuizRequest } from "../../models/quiz";

interface Props {
    quizId?: string;
}

export default observer(function QuizModal({quizId}: Props) {
    const {quizStore, modalStore} = useStore();
    const {oneQuiz} = quizStore;
    const [editState, setEditState] = useState(false);
    const [selectedQuiz, setSelectedQuiz] = useState<Quiz | null>(null);
    const [replacePreviousVersion, setReplacePreviousVersion] = useState(false);

    useEffect(() => {
        if(quizId !== null) quizStore.getOne(quizId!).then(() => setSelectedQuiz(Object.assign({}, quizStore.oneQuiz)));
        else {
            setEditState(true);
        }
    }, []);

    const handleCloseButtonClick = () => modalStore.closeModal();

    const handleCancelEditButtonClick = () => {
        setEditState(false);
        setSelectedQuiz(quizStore.oneQuiz);
    }

    const handleEditButtonClick = () => {
        setEditState(true);
        setSelectedQuiz(Object.assign({}, quizStore.oneQuiz));
    }

    const handleSaveButtonClick = () => {
        const request: UpdateQuizRequest = {
            replacePrevoiusVersion: replacePreviousVersion,
            quiz: selectedQuiz!
        };
        setEditState(false);
        quizStore.updateQuiz(request);
    }

    const handlerEditableKeyDown = (evnt: React.KeyboardEvent<HTMLDivElement> | React.KeyboardEvent<HTMLSpanElement>, index: number, field: string) => {
        const val = evnt.currentTarget.innerText;

        if(field === 'title') {
            selectedQuiz!.title = val;
        }if(field === 'content') {
            selectedQuiz!.questions[index].contentText = val;
        }else {
            selectedQuiz!.questions![index].answers![Number(field)] = val;
        }
        Object.assign({}, selectedQuiz);
        setSelectedQuiz(selectedQuiz);
    }

    if(quizStore.loading) return <></>;

    selectedQuiz?.questions.forEach(q => {if(q.QuestionType === QuestionType.Closed) q.answers = ['a', 'b', 'c', 'd'];});

    return (
        <div className="modal-window">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <div id="quiz-display">
                    <p contentEditable={editState} className={editState ? "editable": ""} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, 0, 'title')}>{selectedQuiz?.title}</p>
                    {selectedQuiz?.questions.map((question, index) => 
                        <div className="question-whole">
                            <div className="question-content">
                                <span contentEditable={editState} className={editState ? "editable": ""} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, index, 'content')}>{question.contentText}</span>
                                {question.contentImageUrl !== null
                                    ? <img src={require(question.contentImageUrl!)}/>
                                    : <></>
                                }
                            </div>
                            <div className={question.QuestionType === QuestionType.Open ? "question-answers" : "question-answers closed"}>
                                {question.QuestionType === QuestionType.Open
                                    ? <div id="open-answer" contentEditable={editState} className={editState ? "editable": ""}>otwarte</div>
                                    : <>
                                        {/* <div className={editState ? "closed-answer editable": "closed-answer"} contentEditable={editState} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, index, '0')}>{question.answers![0]}</div>
                                        <div className={editState ? "closed-answer editable": "closed-answer"} contentEditable={editState} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, index, '1')}>{question.answers![1]}</div>
                                        <div className={editState ? "closed-answer editable": "closed-answer"} contentEditable={editState} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, index, '2')}>{question.answers![2]}</div>
                                        <div className={editState ? "closed-answer editable": "closed-answer"} contentEditable={editState} onKeyDown={(evnt) => handlerEditableKeyDown(evnt, index, '3')}>{question.answers![3]}</div> */}
                                      </>
                                }
                            </div>                            
                        </div>    
                    )}
                </div>
                <div id="quiz-edit-buttons">
                    {!editState
                        ? <button onClick={handleEditButtonClick}>Edit</button>
                        : <>
                            <div id="prev-version">
                                <div 
                                    className={replacePreviousVersion ? "my-checkbox active" : "my-checkbox"}
                                    onClick={() => setReplacePreviousVersion(!replacePreviousVersion)}
                                >

                                </div>
                                <span>Replace previous version</span>
                            </div>
                            <button onClick={handleCancelEditButtonClick}>
                                Cancel
                            </button>
                            <button onClick={handleSaveButtonClick}>Save</button>
                          </>
                    }               
                </div>
            </div>
        </div>
    )
})