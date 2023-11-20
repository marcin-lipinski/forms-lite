export interface GetUserQuizzesResponse {
    quizzes: Quiz[];
}

export interface GetUserQuizResponse {
    quiz: Quiz;
}

export interface CreateQuizResponse {
    quizId: string;
}

export interface CreateQuizRequest {
    quiz: Quiz;
}

export interface UpdateQuizResponse {
    quizId: string;
}

export interface UpdateQuizRequest {
    replacePrevoiusVersion: boolean;
    quiz: Quiz;
}

export interface Quiz {
    id?: string,
    title: string,
    questions: Question[]
}

export interface Question {
    id: string;
    contentImageUrl?: string;
    image?: Blob | null;
    contentText: string;
    questionType: QuestionType;
    answers?: string[];
    correctAnswer?: number;
}

export enum QuestionType {
    Closed,
    Open
}