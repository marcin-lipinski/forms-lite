export interface UserLoginRequest {
    username: string,
    password: string
}

export interface UserRegisterRequest {
    username: string,
    password: string,
    passwordRepeat: string
}

export interface User {
    username: string,
    token: string
}