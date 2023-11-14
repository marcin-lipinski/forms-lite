import { makeAutoObservable } from 'mobx';
import { CreateQuizRequest, Quiz, UpdateQuizRequest } from '../models/quiz';
import agents from '../api/agent';

export default class QuizStore {
    allQuizzes: Quiz[] = [];
    oneQuiz: Quiz | null = null;


    constructor() {
        makeAutoObservable(this);
    };

    getAll = async () => {
        try {
            const result = await agents.Quiz.getAll();
            console.log(result)
            this.allQuizzes = result.quizzes;
        } catch (error) {
            console.log(error)
        }
    };

    getOne = async (id: string) => {
        try {
            const result = await agents.Quiz.getOne(id);
            console.log(result)
            this.oneQuiz = result.quiz;
        } catch (error) {
            console.log(error)
        }
    };

    deleteQuiz = async (id: string) => {
        try {
            await agents.Quiz.deleteQuiz(id);
        } catch (error) {
            console.log(error)
        }
    };

    createQuiz = async (quiz: CreateQuizRequest) => {
        try {
            await agents.Quiz.createQuiz(quiz);
        } catch (error) {
            console.log(error)
        }
    };

    updateQuiz = async (quiz: UpdateQuizRequest) => {
        try {
            await agents.Quiz.updateQuiz(quiz);
        } catch (error) {
            console.log(error)
        }
    };
}