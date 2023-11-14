import { makeAutoObservable, runInAction } from 'mobx';
import { CreateQuizRequest, Quiz, UpdateQuizRequest } from '../models/quiz';
import agents from '../api/agent';

export default class QuizStore {
    allQuizzes: Quiz[] = [];
    oneQuiz: Quiz | null = null;
    loading: boolean = false;


    constructor() {
        makeAutoObservable(this);
    };

    getAll = async () => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Quiz.getAll();
            runInAction(() => this.allQuizzes = result.quizzes);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    getOne = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Quiz.getOne(id);
            runInAction(() => this.oneQuiz = result.quiz);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    deleteQuiz = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Quiz.deleteQuiz(id);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    createQuiz = async (quiz: CreateQuizRequest) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Quiz.createQuiz(quiz);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    updateQuiz = async (quiz: UpdateQuizRequest) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Quiz.updateQuiz(quiz);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    };
}