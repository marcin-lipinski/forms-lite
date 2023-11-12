export interface GetUserQuizzesResponse {
    quizzes: Quiz[];
}

export interface Quiz {
    id: string,
    title: string,
    questions: Question[]
}

export interface Question {
    contentImageUrl: string;
    contentText: string;
    QuestionType: QuestionType;
    questionNumber: number;
    answers: string[];
    correctAnswer: number;
}

export enum QuestionType {
    Closed,
    Open
}