import { observer } from "mobx-react-lite";
import { useState } from "react";
import { useStore } from "../../stores/store";
import './QuizModalCreate.css';
import './QuizModal.css';
import { Question, QuestionType, Quiz } from "../../models/quiz";
import PhotoWidgetDropzone from "../../common/imageUpload/PhotoWidgetDropzone";

export default observer(function QuizModal() {
    const [newQuiz, setNewQuiz] = useState<Quiz>({title: "", questions: []})
    const {quizStore, modalStore} = useStore();

    const handleCloseButtonClick = () => modalStore.closeModal();

    const handleSaveButtonClick = () => {
        quizStore.createQuiz(newQuiz).then(() => modalStore.closeModal());
    }

    const handleNewOpenButtonClick = () => {
        const question: Question = {
            contentText: "",
            questionType: QuestionType.Open,
            questionNumber: newQuiz.questions.length + 1,
            image: null
        };
        newQuiz.questions.push(question)
        setNewQuiz(Object.assign({}, newQuiz));
    }

    const handleDeleteQuestionButton = (index: number) => {
        let questions = newQuiz.questions.filter((x, ind) => ind !== index);
        newQuiz.questions = questions;
        setNewQuiz(Object.assign({}, newQuiz));
    }

    const handleNewClosedButtonClick = () => {
        const question: Question = {
            contentText: "",
            questionType: QuestionType.Closed,
            questionNumber: newQuiz.questions.length + 1,
            image: null,
            answers: ["a", "b", "c", "d"],
            correctAnswer: "a"
        };
        newQuiz.questions.push(question)
        setNewQuiz(Object.assign({}, newQuiz));
    }

    const handlerEditableKeyDown = (evnt: React.FormEvent<HTMLInputElement> | React.ChangeEvent<HTMLTextAreaElement>, index: number, field: string) => {
        const val = (evnt.currentTarget as HTMLInputElement).value;
        console.log(val)
        if(field === 'title') {
            newQuiz!.title = val;
        }else if(field === 'content') {
            newQuiz!.questions[index].contentText = val;
        }else {
            newQuiz!.questions![index].answers![Number(field)] = val;
        }
        setNewQuiz(Object.assign({}, newQuiz));
    }
    
    const setQuestionImage = (file: Blob | null, index: number) => {
        if(file !== null) {
            const quiz = {...newQuiz};
            if (file.type.startsWith('image/')) {
                quiz.questions[index].image = file;
                quiz.questions[index].imagePreview = URL.createObjectURL(file);
                console.log(file + " " + URL.createObjectURL(file))
                setNewQuiz(quiz);
            }           
        }
        else {
            const quiz = {...newQuiz};
            quiz.questions[index].image = null;
            setNewQuiz(quiz);
        };
    }

    if(quizStore.loading) return <></>;

    return (
        <div className="modal-window quiz-create">
            <header>
                <p></p>
                <button onClick={handleCloseButtonClick}>✖</button>
            </header>
            <div className="modal-body">
                <div id="quiz-display">                    
                    <p className={"editable"}>
                        <input placeholder="Title" onChange={(evnt) => handlerEditableKeyDown(evnt, 0, 'title')} value={newQuiz.title}></input>
                    </p>
                    {newQuiz?.questions.map((question, index) => 
                        <div className="question-whole">
                            <div className={"question-content"} style={question.image !== null ? {height: "360px"} : {}}>
                                <div className={"editable"}>
                                    <textarea placeholder="Question" onChange={(evnt) => handlerEditableKeyDown(evnt, index, 'content')} value={question.contentText}/>
                                </div>
                                {question.image === null 
                                    ? <PhotoWidgetDropzone setFiles={setQuestionImage} index={index}/>
                                    : <>
                                        <div className="question-image-contaner">
                                            <img className="question-image" src={question.imagePreview}/>
                                            <button onClick={() => setQuestionImage(null, index)}>🗑</button>
                                        </div>
                                    </>
                                }
                            </div>
                            <div className={question.questionType === QuestionType.Open ? "question-answers" : "question-answers closed"}>
                                {question.questionType === QuestionType.Open
                                    ? <div id="open-answer">User answer</div>
                                    : <>
                                        <div className={"closed-answer editable"}>
                                            <textarea onChange={(evnt) => handlerEditableKeyDown(evnt, index, '0')} value={question.answers![0]}/>
                                        </div>
                                        <div className={"closed-answer editable"} >
                                            <textarea onChange={(evnt) => handlerEditableKeyDown(evnt, index, '1')} value={question.answers![1]}/>
                                        </div>
                                        <div className={"closed-answer editable"}>
                                            <textarea onChange={(evnt) => handlerEditableKeyDown(evnt, index, '2')} value={question.answers![2]}/>
                                        </div>
                                        <div className={"closed-answer editable"}>
                                            <textarea onChange={(evnt) => handlerEditableKeyDown(evnt, index, '3')} value={question.answers![3]}/>
                                        </div>
                                      </>
                                }
                            </div>
                            <button className="question-delete-button" onClick={() => handleDeleteQuestionButton(index)}>🗑</button>                     
                        </div>    
                    )}
                    <div className="add-quiz-button">
                        <button onClick={handleNewOpenButtonClick}>+<span>Open question</span></button>
                        <button onClick={handleNewClosedButtonClick}>+<span>Closed question</span></button>
                    </div>
                </div>
                <div id="quiz-edit-buttons">
                    <button onClick={handleSaveButtonClick} style={{backgroundColor: "rgb(22, 223, 99)"}}>Save</button>
                </div>
            </div>
        </div>
    )
})