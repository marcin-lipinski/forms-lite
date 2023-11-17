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
            runInAction(() => this.loading = true);
            const result = await agents.Session.getAll();            
            runInAction(() => this.allSessions = result.sessions);
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    getOne = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Session.getOne(id);
            runInAction(() => this.oneSession = result.session);            ;
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    createSession = async (id: string, data: CreateSessionRequest) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Session.createSession(id, data);
            runInAction(() => this.partakeSessionUrl = result.sessionPartakeUrl);
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    finishSession = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Session.finishSession(id);
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    partakeSession = async (id: string) => {
        try {
            runInAction(() => this.loading = true);
            const result = await agents.Session.partakeSession(id);
            runInAction(() => this.partakeQuiz = result.quiz);            
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    partakeSessionFinish = async (id: string, data: PartakeSessionFinishRequest) => {
        try {
            runInAction(() => this.loading = true);
            await agents.Session.partakeSessionFinish(id, data);
        } catch (error) {
            throw new Error();
        } finally {
            runInAction(() => this.loading = false);
        }
    }
}