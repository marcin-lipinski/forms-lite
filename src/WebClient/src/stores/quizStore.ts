import { makeAutoObservable, runInAction } from 'mobx';
import { Quiz, UpdateQuizRequest } from '../models/quiz';
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
            throw new Error();
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
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    deleteQuiz = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Quiz.deleteQuiz(id);
            runInAction(() => this.allQuizzes = this.allQuizzes.filter(x => x.id !== id));
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    createQuiz = async (quiz: Quiz) => {
        try {
            runInAction(() => this.loading = true);
            let form = new FormData();
            form.append('quiz.title', quiz.title);
            quiz.questions.forEach((q, indexq) => {
                form.append('quiz.questions[' + indexq + '].contentText', q.contentText.toString());
                form.append('quiz.questions[' + indexq + '].questionType', q.questionType.toString());
                form.append('quiz.questions[' + indexq + '].id', q.id);
                form.append('quiz.questions[' + indexq + '].image', q.image || '');
                if(q.answers !== null) q.answers?.forEach((a, index) => form.append('quiz.questions[' + indexq + '].answers['+ index + ']', a));             
                form.append('quiz.questions[' + indexq + '].correctAnswer', q.correctAnswer?.toString() || '');
            });

            await agents.Quiz.createQuiz(form);
            await this.getAll();
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };

    updateQuiz = async (id: string, data: UpdateQuizRequest) => {
        try {
            runInAction(() => this.loading = true);
            let form = new FormData();
            form.append('replacePreviousVersion', data.replacePrevoiusVersion ? "true" : "false");
            form.append('quiz.title', data.quiz.title);
            data.quiz.questions.forEach((q, indexq) => {
                form.append('quiz.questions[' + indexq + '].contentText', q.contentText.toString());
                form.append('quiz.questions[' + indexq + '].questionType', q.questionType.toString());
                form.append('quiz.questions[' + indexq + '].id', q.id);
                form.append('quiz.questions[' + indexq + '].image', q.image || '');
                form.append('quiz.questions[' + indexq + '].contentImageUrl', q.contentImageUrl || '');
                if(q.answers !== null) q.answers?.forEach((a, index) => form.append('quiz.questions[' + indexq + '].answers['+ index + ']', a));             
                form.append('quiz.questions[' + indexq + '].correctAnswer', q.correctAnswer?.toString() || '');
            });

            await agents.Quiz.updateQuiz(id, form);
            await this.getAll();
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    };
}