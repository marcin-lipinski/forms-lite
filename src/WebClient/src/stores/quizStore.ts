import { makeAutoObservable } from 'mobx';
import { store } from './store';
import { Quiz } from '../models/quiz';
import agents from '../api/agent';

export default class QuizStore {
    allQuizes: Quiz[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    getAll = async () => {
        try {
            const result = await agents.Quiz.getAll();
            console.log(result)
            this.allQuizes = result.quizzes;
        } catch (error) {
            console.log(error)
        }
    }
}