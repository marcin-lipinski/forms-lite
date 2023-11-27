import axios, { AxiosError, AxiosResponse } from "axios";
import { store } from "../stores/store";
import { useNavigate } from "react-router-dom";
import { User, UserLoginRequest, UserRegisterRequest } from "../models/user";
import { CreateQuizResponse, GetUserQuizResponse, GetUserQuizzesResponse, UpdateQuizRequest, UpdateQuizResponse } from "../models/quiz";
import { CreateSessionRequest, CreateSessionResponse, GetUserSessionResponse, GetUserSessionsResponse, PartakeSessionFinishRequest, PartakeSessionResponse } from "../models/session";

//axios.defaults.baseURL = "https://formslite.azurewebsites.net/";
axios.defaults.baseURL = "https://localhost:7015/";

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
                break;
            case 401:
                if (data.errors && data.message !== "NoCurrentUserException") {
                    const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    store.dialogStore.showError(modalStateErrors.flat())
                }
                break;
            case 403:
                if (data.errors) {
                    const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    store.dialogStore.showError(modalStateErrors.flat())
                }
                break;
            case 404:
                useNavigate()(`/notfound`);
                break
            case 500:
                if (data.errors) {
                    const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key]);
                        }
                    }
                    store.dialogStore.showError(modalStateErrors.flat())
                }
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
    current:  () => requests.get<User>("api/user/current")
};

const Quiz = {
    getAll:     ()                        => requests.get<GetUserQuizzesResponse>("api/quiz/get"),    
    getOne:     (id: string)              => requests.get<GetUserQuizResponse>(`api/quiz/get/${id}`),
    deleteQuiz: (id: string)              => requests.del(`api/quiz/delete/${id}`),
    createQuiz: (quiz: FormData) => axios.post<CreateQuizResponse>("api/quiz/create", quiz, {headers: {'Content-Type': 'multipart/form-data'}}),
    updateQuiz: (id:string, quiz: FormData) => axios.post<UpdateQuizResponse>(`api/quiz/update/${id}`, quiz, {headers: {'Content-Type': 'multipart/form-data'}})
};

const Session = {
    getAll:               ()              => requests.get<GetUserSessionsResponse>("api/session/get"),
    getOne:               (id: string)    => requests.get<GetUserSessionResponse>(`api/session/get/${id}`),
    createSession:        (id: string, data: CreateSessionRequest)    => requests.put<CreateSessionResponse>(`api/session/create/${id}`, data),
    finishSession:        (id: string)    => requests.post(`api/session/finish/${id}`, {}),
    deleteSession:        (id: string)    => requests.del(`api/session/delete/${id}`),
    partakeSession:       (id: string)    => requests.post<PartakeSessionResponse>(`api/session/partake/start/${id}`, {}),
    partakeSessionFinish: (id: string, answers: PartakeSessionFinishRequest) => requests.post(`api/session/partake/finish/${id}`, answers)
}

const agents = {
    Account,
    Quiz,
    Session
};

export default agents;