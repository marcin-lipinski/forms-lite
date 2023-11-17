import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useStore } from "../../../stores/store";
import { Question, QuestionType, Quiz, UpdateQuizRequest } from "../../../models/quiz";
import Laoder from "../../../common/loader/Loader";
import PhotoWidgetDropzone from "../../../common/imageUpload/PhotoWidgetDropzone";
import './QuizModal.css';
import './QuizModalCreate.css';

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
        if(modeState === 'view' && quiz !== null) setCurrentQuiz(quiz!);
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
            quizStore.updateQuiz(request).then(() => setModeState('view')).catch(() => {});
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
            contentText: "",
            questionType: QuestionType.Open,
            questionNumber: currentQuiz!.questions.length + 1,
            image: null,
            contentImageUrl: "",
            imagePreview: ""
        };
        currentQuiz!.questions.push(question)
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const handleNewClosedButtonClick = () => {
        const question: Question = {
            contentText: "",
            questionType: QuestionType.Closed,
            questionNumber: currentQuiz!.questions.length + 1,
            image: null,
            contentImageUrl: "",
            imagePreview: "",
            answers: ["a", "b", "c", "d"],
            correctAnswer: "a"
        };
        currentQuiz!.questions.push(question)
        setCurrentQuiz(Object.assign({}, currentQuiz));
    }

    const setQuestionImage = (file: Blob | null, index: number) => {
        if(file !== null) {
            if (file.type.startsWith('image/')) {
                currentQuiz!.questions[index].image = file;
                currentQuiz!.questions[index].imagePreview = URL.createObjectURL(file);
                setCurrentQuiz(Object.assign({}, currentQuiz));
            }           
        }
        else {
            currentQuiz!.questions![index].image = null;
            currentQuiz!.questions![index].imagePreview = null;
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
                        <input disabled={modeState === 'view'} placeholder="Title" onChange={(evnt) => handlerEditableKeyDown(evnt, 0, 'title')} value={currentQuiz!.title ?? ""}></input>
                    </p>
                    {currentQuiz!.questions.map((question, index) => 
                        <div className="question-whole">
                            <div className={"question-content"} style={question.image !== null || question.contentImageUrl?.length !== 0 ? {height: "360px"} : {}}>
                                <div className={modeState !== 'view' ? "editable" : ""}>
                                    <textarea disabled={modeState === 'view'} placeholder="Question" onChange={(evnt) => handlerEditableKeyDown(evnt, index, 'content')} value={question.contentText}/>
                                </div>
                                {modeState === 'view'
                                    ? <>
                                        {(question.contentImageUrl?.length !== 0 || question.image !== null)
                                            ? <div className="question-image-contaner">
                                                <img className="question-image" src={question.contentImageUrl!.length > 0 ? question.contentImageUrl : question.imagePreview}/>
                                            </div>
                                            : <></>
                                        }
                                      </>
                                    : <>
                                        {(question.contentImageUrl?.length !== 0 || question.image !== null)
                                            ? <div className="question-image-contaner">
                                                <img className="question-image" src={question.image !== null ? question.imagePreview : question.contentImageUrl}/>
                                                <button onClick={() => setQuestionImage(null, index)}>🗑</button>
                                              </div>
                                            : <PhotoWidgetDropzone setFiles={setQuestionImage} index={index}/>                                            
                                        }
                                      </>
                                }
                            </div>
                            <div className={question.questionType === QuestionType.Open ? "question-answers" : "question-answers closed"}>
                                {question.questionType === QuestionType.Open
                                    ? <div id="open-answer">User answer</div>
                                    : <>
                                        <div className={`closed-answer ${modeState !== 'view' ? "editable" : ""}`}>
                                            <textarea disabled={modeState === 'view'} onChange={(evnt) => handlerEditableKeyDown(evnt, index, '0')} value={question.answers![0]}/>
                                        </div>
                                        <div className={`closed-answer ${modeState !== 'view' ? "editable" : ""}`} >
                                            <textarea disabled={modeState === 'view'} onChange={(evnt) => handlerEditableKeyDown(evnt, index, '1')} value={question.answers![1]}/>
                                        </div>
                                        <div className={`closed-answer ${modeState !== 'view' ? "editable" : ""}`}>
                                            <textarea disabled={modeState === 'view'} onChange={(evnt) => handlerEditableKeyDown(evnt, index, '2')} value={question.answers![2]}/>
                                        </div>
                                        <div className={`closed-answer ${modeState !== 'view' ? "editable" : ""}`}>
                                            <textarea disabled={modeState === 'view'} onChange={(evnt) => handlerEditableKeyDown(evnt, index, '3')} value={question.answers![3]}/>
                                        </div>
                                      </>
                                }
                            </div>
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