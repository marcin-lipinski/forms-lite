import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useStore } from "../../../stores/store";
import { Question, QuestionType, Quiz, UpdateQuizRequest } from "../../../models/quiz";
import Laoder from "../../../common/loader/Loader";
import PhotoWidgetDropzone from "../../../common/imageUpload/PhotoWidgetDropzone";
import './QuizModal.css';
import QuestionAnswers from "../QuestionAnswers";
import { Guid } from "js-guid";

interface Props {
    quiz?: Quiz;
    mode: string;
}

export default observer(function QuizModal({quiz, mode}: Props) {
    const {quizStore, modalStore} = useStore();
    const [modeState, setModeState] = useState(mode);
    const [currentQuiz, setCurrentQuiz] = useState<Quiz | null>({title: "", questions: []});
    const [replacePreviousVersion, setReplacePreviousVersion] = useState(false);


    useEffect(() => {
        if(modeState === 'view' && quiz !== null) setCurrentQuiz(Object.assign({}, quiz!));
        if(modeState === 'create') setCurrentQuiz({title: "", questions: []});
    }, []);

    const handleCloseButtonClick = () => modalStore.closeModal();

    const handleCancelEditButtonClick = () => {
        if(modeState === 'create') handleCloseButtonClick();
        else {            
            setModeState('view');
            setCurrentQuiz(Object.assign({}, quiz));
        }
    }

    const handleEditButtonClick = () => { 
        setModeState('edit');
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const handleSaveButtonClick = () => {
        if(modeState === 'create') {
            quizStore.createQuiz(currentQuiz!).then(() => modalStore.closeModal()).catch(() => {});
        }
        else if(modeState === 'edit') {
            const request: UpdateQuizRequest = {replacePrevoiusVersion: replacePreviousVersion, quiz: currentQuiz!};
            quizStore.updateQuiz(quiz!.id!, request).then(() => modalStore.closeModal()).catch(() => {});
        }
    }

    const handlerEditableKeyDown = (evnt: React.FormEvent<HTMLInputElement> | React.ChangeEvent<HTMLTextAreaElement>, index: number, field: string) => {
        const val = (evnt.currentTarget as HTMLInputElement).value;

        if(field === 'title') {
            currentQuiz!.title = val;
        }else if(field === 'content') {
            currentQuiz!.questions[index].contentText = val;
        }else {
            currentQuiz!.questions![index].answers![Number(field)] = val;
        }
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const handleDeleteQuestionButton = (index: number) => {
        let questions = currentQuiz!.questions.filter((x, ind) => ind !== index);
        currentQuiz!.questions = questions;
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }
    const handleNewOpenButtonClick = () => {
        const question: Question = {
            id: Guid.newGuid().toString(),
            contentText: "",
            questionType: QuestionType.Open,
            image: null,
            contentImageUrl: ""
        };
        currentQuiz!.questions.push(question)
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const handleNewClosedButtonClick = () => {
        const question: Question = {
            id: Guid.newGuid().toString(),
            contentText: "",
            questionType: QuestionType.Closed,
            image: null,
            contentImageUrl: "",
            answers: ["a", "b", "c", "d"],
            correctAnswer: 0
        };
        currentQuiz!.questions.push(question)
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const setQuestionImage = (file: Blob | null, index: number) => {
        if(file !== null) {
            if (file.type.startsWith('image/')) {
                currentQuiz!.questions[index].image = file;
                currentQuiz!.questions[index].contentImageUrl = URL.createObjectURL(file);
                setCurrentQuiz(Object.assign({}, currentQuiz));
            }           
        }
        else {
            currentQuiz!.questions![index].image = null;
            currentQuiz!.questions![index].contentImageUrl = "";
            setCurrentQuiz(Object.assign({}, currentQuiz));
        };
    }

    if(quizStore.loading) return <Laoder/>;
    
    return (
        <div className="modal-window quiz-view">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <div id="quiz-display">                    
                    <p className={modeState !== 'view' ? "editable" : ""}>
                        <input max={40} disabled={modeState === 'view'} placeholder="Title" onChange={(evnt) => handlerEditableKeyDown(evnt, 0, 'title')} value={currentQuiz!.title ?? ""}></input>
                    </p>
                    {currentQuiz!.questions.map((question, index) => 
                        <div className="question-whole">
                            <div className={"question-content"} style={question.image !== null || question.contentImageUrl?.length !== 0 ? {height: "360px"} : {}}>
                                <div className={modeState !== 'view' ? "editable" : ""}>
                                    <textarea maxLength={40} disabled={modeState === 'view'} placeholder="Question" onChange={(evnt) => handlerEditableKeyDown(evnt, index, 'content')} value={question.contentText}/>
                                </div>
                                {modeState === 'view'
                                    ? <>
                                        {(question.contentImageUrl?.length !== 0 || question.image !== null)
                                            ? <div className="question-image-contaner">
                                                <img className="question-image" src={question.contentImageUrl}/>
                                            </div>
                                            : <></>
                                        }
                                      </>
                                    : <>
                                        {(question.contentImageUrl?.length !== 0 || question.image !== null)
                                            ? <div className="question-image-contaner">
                                                <img className="question-image" src={question.contentImageUrl}/>
                                                <button onClick={() => setQuestionImage(null, index)}>🗑</button>
                                              </div>
                                            : <PhotoWidgetDropzone setFiles={setQuestionImage} index={index}/>                                            
                                        }
                                      </>
                                }
                            </div>
                            <QuestionAnswers
                                question={question}
                                index={index}
                                modeState={modeState}
                                keyDownOnEditableHandler={handlerEditableKeyDown}
                            />
                            {modeState !== 'view'
                                ? <button className="question-delete-button" onClick={() => handleDeleteQuestionButton(index)}>🗑</button>
                                : <></>
                            }                                                 
                        </div>    
                    )}
                    {modeState !== 'view'
                        ? <div className="add-quiz-button">
                            <button onClick={handleNewOpenButtonClick}>+<span>Open question</span></button>
                            <button onClick={handleNewClosedButtonClick}>+<span>Closed question</span></button>
                          </div>
                        : <></>
                    }
                </div>
                <div id="quiz-edit-buttons">
                    {modeState === 'view'
                        ? <button onClick={handleEditButtonClick}>Edit</button>
                        : <>
                            {modeState === 'edit'
                                ? <div id="prev-version">
                                    <div className={replacePreviousVersion ? "my-checkbox active" : "my-checkbox"} onClick={() => setReplacePreviousVersion(!replacePreviousVersion)}/>
                                    <span>Replace previous version</span>
                                </div>
                                : <></>
                            }
                            <button onClick={handleCancelEditButtonClick}>Cancel</button>
                            <button onClick={handleSaveButtonClick}>Save</button>
                          </>
                    }
                </div>
            </div>
        </div>
    )
})