import { Quiz } from "./quiz";

export interface CreateSessionRequest {
    startTime: string;
    finishTime: string;
}

export interface CreateSessionResponse {
    sessionPartakeUrl: string;
}

export interface GetUserSessionsResponse {
    sessions: Session[];
}

export interface GetUserSessionResponse {
    session: Session;
}

export interface Session {
    id: string;
    joinUrl: string;
    quizTitle: string;
    startTime: Date;
    finishTime: Date;
    isActive: boolean;
    answers: PartakeSessionFinishRequest[];
    questions: SessionQuestion[];
    quizId: string;
}

export interface PartakeSessionResponse {
    quiz: Quiz;
}

export interface PartakeSessionFinishRequest {
    participantName: string;
    answers: SessionPartakeAnswer[];
}

export interface SessionQuestion {
    contentText: string;
    id: string;
}

export interface SessionPartakeAnswer {
    id: string;
    questionAnswer: string;
}