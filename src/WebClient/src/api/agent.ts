import axios, { AxiosError, AxiosResponse } from "axios";
import { store } from "../stores/store";
import { useNavigate } from "react-router-dom";
import { User, UserLoginRequest, UserRegisterRequest } from "../models/user";
import { CreateQuizRequest, CreateQuizResponse, GetUserQuizResponse, GetUserQuizzesResponse, UpdateQuizRequest, UpdateQuizResponse } from "../models/quiz";

axios.defaults.baseURL = 'https://localhost:7015/';//process.env.REACT_APP_API_URL;

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
                if (config.method === `get` && data.errors.hasOwnProperty(`id`)) {
                     useNavigate()(`/notfound`);
                }
                if (data.errors) {
                    const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    store.dialogStore.showError(modalStateErrors.flat())
                }
            case 401:
                //
                break;
            case 403:
                //toast.error(`forbidden`);
                break;
            case 404:
                useNavigate()(`/notfound`);
                break
            case 500:
                store.dialogStore.showError(data.errors)
                break;
        }
        return Promise.reject(error);
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
    register: (user: UserRegisterRequest) => requests.put<User>("api/user/register", user),
    login:    (user: UserLoginRequest)    => requests.post<User>("api/user/login", user),
    // current: () => requests.get<IUser>("user/current")
};

const Quiz = {
<<<<<<< HEAD
    getAll:     ()                        => requests.get<GetUserQuizzesResponse>("api/quiz/get"),    
    getOne:     (id: string)              => requests.get<GetUserQuizResponse>(`api/quiz/get/${id}`),
    deleteQuiz: (id: string)              => requests.del(`api/quiz/delete/${id}`),
    createQuiz: (quiz: CreateQuizRequest) => axios.put<CreateQuizResponse>("api/quiz/create", quiz, {headers: {'Content-Type': 'multipart/form-data'}}),
    updateQuiz: (quiz: UpdateQuizRequest) => axios.post<UpdateQuizResponse>("api/quiz/update", quiz, {headers: {'Content-Type': 'multipart/form-data'}})
=======
    getAll: () => requests.get<GetUserQuizzesResponse>("api/quiz/get"),
    createQuiz: (quiz: CreateQuizRequest) => axios.put("api/quiz/create", quiz, {headers: {'Content-Type': 'multipart/form-data'}}),
    updateQuiz: (quiz: UpdateQuizRequest) => axios.post("api/quiz/update", quiz, {headers: {'Content-Type': 'multipart/form-data'}}),
    deleteQuiz: (id: string) => requests.del(`api/quiz/delete/${id}`),
    getOne: (id: string) => requests.get<IPagedResult<IQuizOverview>>(`api/quiz/get/${id}`),
>>>>>>> 76d6e37c7c25f138914c46a3062fa1be43c39653
};

const Session = {
<<<<<<< HEAD
    getAll:               ()              => requests.get<>("api/session/get"),
    getOne:               (id: string)    => requests.get<>(`api/session/get/${id}`),
    createSession:        (id: string, settings: INewSessionsSettings) => requests.post<>(`api/session/start/${id}`, settings),
    finishSession:        (id: string, settings: INewSessionsSettings) => requests.post<>(`api/session/finish/${id}`, settings),
    partakeSession:       (id: string)               => requests.post<>(`api/session/finish/${id}`, settings),
    partakeSessionFinish: (id: string, answers: any) => requests.post<>(`api/session/finish/${id}`, answers)
=======
    getAll: (params: ISearchParams) => requests.get<IPagedResult<IQuizOverview>>("api/session/get"),
    getOne: (id: string) => requests.get<IPagedResult<IQuizOverview>>(`api/session/get/${id}`),
    createSession: (id: string, settings: INewSessionsSettings) => requests.post<string>(`api/session/start/${id}`, settings),
    finishSession: (id: string, settings: INewSessionsSettings) => requests.post<string>(`api/session/finish/${id}`, settings),
    partakeSession: (id: string) => requests.post<string>(`api/session/finish/${id}`, settings),
    partakeSessionFinish: (id: string, answers: any) => requests.post<string>(`api/session/finish/${id}`, answers)

>>>>>>> 76d6e37c7c25f138914c46a3062fa1be43c39653
}

const agents = {
    Account,
    Quiz,
    Session
};

export default agents;