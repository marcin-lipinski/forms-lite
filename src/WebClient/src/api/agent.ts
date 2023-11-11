import axios, { AxiosError, AxiosResponse } from "axios";
import { store } from "../stores/store";
import { IUserLoginValues, IUserRegisterValues, IUser, IUserPasswordChange, IUserUsernameChange } from "../models/user";
import { IFavouriteChange, IPagedResult, IQuizOverview, ISearchParams } from "../stores/quizBrowserStore";
import { IOpinion } from "../common/quiz/browser-opinions/OpinionsModal";
import { IQuizDetails } from "../stores/quizDetailsStore";
import { ISessionQuizPreview } from "../stores/newSessionStore";
import { INewSessionsSettings } from "../pages/new-game/NewSessionModal";
import { IOpenQuestion, IRatingQuestion, ISelectionQuestion, ITrueOrFalseQuestion } from "../models/quiz";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.request.use((config) => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

axios.interceptors.response.use(
    async (response) => {
        return response;
    },
    (error: AxiosError) => {
        const { data, status, config } = error.response as AxiosResponse;
        switch (status) {
            case 400:
                // if (config.method === `get` && data.errors.hasOwnProperty(`id`)) {
                //     router.navigate(`/notfound`);
                // }
                // if (data.errors) {
                //     const modalStateErrors = [];
                //     for (const key in data.errors) {
                //         if (data.errors[key]) {
                //             modalStateErrors.push(data.errors[key]);
                //         }
                //     }
                //     throw modalStateErrors.flat();
                // } else {
                //     //toast.error(data);
                // }
                throw new Error(data.message);
            case 401:
                throw new Error(data.message);
            case 403:
                //toast.error(`forbidden`);
                break;
            case 404:
                throw new Error(data.message);
                //router.navigate(`/notfound`);
            case 500:
                //store.commonStore.setServerError(data.error);
                //router.navigate(`servererror`);
                break;
        }
        // return Promise.reject(error);
    }
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Account = {
    register: (user: IUserRegisterValues) => requests.post<IUser>("user/register", user),
    login: (user: IUserLoginValues) => requests.post<IUser>("user/login", user),
    current: () => requests.get<IUser>("user/current"),
    uploadPhoto: (form: FormData) => axios.post<string>('user/change/image', form, {headers: {'Content-Type': 'multipart/form-data'}}),
    changePassword: (values: IUserPasswordChange) => requests.post('user/change/password', values),
    changeUsername: (values: IUserUsernameChange) => requests.post('user/change/username', values)
};

const Quiz = {
    getAllPublic: (params: ISearchParams) => requests.post<IPagedResult<IQuizOverview>>("quiz/public", params),
    saveQuiz: (quiz: FormData) => axios.post("quiz/create", quiz, {headers: {'Content-Type': 'multipart/form-data'}}),
    addDeleteFavourite: (data: IFavouriteChange) => requests.post("quiz/favourite", data),
    getOpinions: (quizId: string) => requests.get<IOpinion[]>(`quiz/opinions/${quizId}`),
    details: (quizId: string) => requests.get<(ISelectionQuestion | IOpenQuestion | ITrueOrFalseQuestion | IRatingQuestion)[]>(`quiz/details/${quizId}`),
    getUserQuizzes: (params: ISearchParams) => requests.post<IPagedResult<IQuizOverview>>("quiz/user", params),
    getSaved: (params: ISearchParams) => requests.post<IPagedResult<IQuizOverview>>('quiz/saved', params)
};

const Session = {
    getQuizzesPreview: (params: ISearchParams) => requests.post<IPagedResult<ISessionQuizPreview>>("session/get", params),
    createSession: (settings: INewSessionsSettings) => requests.post<string>("session/new", settings)
}

const agents = {
    Account,
    Quiz,
    Session
};

export default agents;