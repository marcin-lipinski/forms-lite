import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useStore } from '../../../stores/store';
import { UserLoginRequest } from '../../../models/user';
import './LoginForm.css';

export default function LoginForm() {
    const {userStore} = useStore();
    const [loginData, setLoginData] = useState<UserLoginRequest>({username: "", password: ""})
    const navigate = useNavigate();

    const inputChangeHandler = (evnt: React.ChangeEvent<HTMLInputElement>) => {
        const field = evnt.currentTarget.name;
        const value = evnt.currentTarget.value;
        loginData[field as keyof UserLoginRequest] = value;
        setLoginData(Object.assign({}, loginData));
    }

    const submitFormHandler = () => {
        userStore.login(loginData).then(() => navigate('/home'));
    }

    return (
        <>
            <div className='form'>
                <input name='username' placeholder="Username" onChange={inputChangeHandler} value={loginData.username}></input>
                <input name='password' placeholder="Password" onChange={inputChangeHandler} value={loginData.password} type="password"></input>
                <button type='submit' onClick={(submitFormHandler)}>Login</button>
            </div>
        </>
    )
}