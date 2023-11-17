import { useState } from "react"
import { useStore } from "../../../stores/store";
import { useNavigate } from "react-router-dom";
import { UserRegisterRequest } from "../../../models/user";
import Laoder from "../../../common/loader/Loader";
import { observer } from "mobx-react-lite";
import './LoginForm.css';

export default observer(function RegisterForm() {
    const {userStore} = useStore();
    const [registerData, setRegisterData] = useState<UserRegisterRequest>({username: "", password: "", passwordRepeat: ""})
    const navigate = useNavigate();

    const inputChangeHandler = (evnt: React.ChangeEvent<HTMLInputElement>) => {
        const field = evnt.currentTarget.name;
        const value = evnt.currentTarget.value;
        registerData[field as keyof UserRegisterRequest] = value;
        setRegisterData(Object.assign({}, registerData));
    }
    
    const submitFormHandler = () => {
        userStore.register(registerData).then(() => navigate('/home')).catch(() => {});;
    }

    if(userStore.loading) return <Laoder/>;

    return (
        <>
            <div className="form">
            <input name='username' placeholder="Username" onChange={inputChangeHandler} value={registerData.username}></input>
                <input name='password' placeholder="Password" onChange={inputChangeHandler} value={registerData.password} type="password"></input>
                <input name='passwordRepeat' placeholder="Password repeat" onChange={inputChangeHandler} value={registerData.passwordRepeat} type="password"></input>
                <button onClick={submitFormHandler}>Register</button>
            </div>
        </>
    )
})