export interface CreateSessionRequest {
    quizId: string;
    startTime: Date;
    finishTime: Date;
}

export interface CreateSessionRequest {
    sessionPartakeUrl: string;
}

export interface GetUserSessionsResponse
{
    dessions: Session[];
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
    quizDto: Quiz;
}

export interface PartakeSessionResultRequest {
    participant: string;
    answers: SessionPartakeAnswer[];
}

export interface SessionPartakeAnswer {
    questionNumber: number;
    questionAnswer: string;
}