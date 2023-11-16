import { makeAutoObservable, runInAction } from 'mobx';
import agents from '../api/agent';
import { CreateSessionRequest, PartakeSessionFinishRequest, Session } from '../models/session';
import { Quiz } from '../models/quiz';

export default class SessionStore {
    allSessions: Session[] = [];
    oneSession: Session | null = null;
    partakeSessionUrl: string = "";
    partakeQuiz: Quiz | null = null;
    loading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    getAll = async () => {
        try {
            const result = await agents.Session.getAll();
            console.log(result)
            this.allSessions = result.sessions;
        } catch (error) {
            console.log(error)
        }
    }

    getOne = async (id: string) => {
        try {
            const result = await agents.Session.getOne(id);
            console.log(result)
            this.oneSession = result.session;
        } catch (error) {
            console.log(error)
        }
    }

    createSession = async (id: string, data: CreateSessionRequest) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Session.createSession(id, data);   
            console.log(result)         
            runInAction(() => this.partakeSessionUrl = result.sessionPartakeUrl);
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    finishSession = async (id: string) => {
        try {
            const result = await agents.Session.finishSession(id);
            console.log(result)
        } catch (error) {
            console.log(error)
        }
    }

    partakeSession = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Session.partakeSession(id);
            runInAction(() => this.partakeQuiz = result.quiz);            
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    partakeSessionFinish = async (id: string, data: PartakeSessionFinishRequest) => {
        try {
            const result = await agents.Session.partakeSessionFinish(id, data);
            console.log(result)
        } catch (error) {
            console.log(error)
        }
    }
}