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
    quizTitle: string;
    startTime: Date;
    finishTime: Date;
    isActive: boolean;
    answersAmount: number;
    quizId: string;
}

export interface PartakeSessionResponse {
    quiz: Quiz;
}

export interface PartakeSessionFinishRequest {
    participant: string;
    answers: SessionPartakeAnswer[];
}

export interface SessionPartakeAnswer {
    questionNumber: number;
    questionAnswer: string;
}