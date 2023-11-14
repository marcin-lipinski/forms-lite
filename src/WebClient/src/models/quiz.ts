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
    contentImageUrl?: string;
    image?: Blob | null;
    imagePreview?: any;
    contentText: string;
    QuestionType: QuestionType;
    questionNumber: number;
    answers?: string[];
    correctAnswer?: string;
}

export enum QuestionType {
    Closed,
    Open
}