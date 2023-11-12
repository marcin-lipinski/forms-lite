import { useState } from 'react';
import LoginForm from './login-form/LoginForm';
import RegisterForm from './register-form/RegisterForm';
import './LoginPage.css';

export default function LoginPage() {
    const [state, setState] = useState('login');

    const handleStateButtonClick = (evnt: React.MouseEvent<HTMLButtonElement>) => {
        const button = evnt.currentTarget as HTMLButtonElement;
        if(button.name === 'login') {
            setState('login');
            button.classList.add('selected');
            button.nextElementSibling?.classList.remove('selected');
        }
        else {
            setState('register');
            button.classList.add('selected');
            button.previousElementSibling?.classList.remove('selected');
        }
    }

    return (
        <div className="app-page login">
            <div className="column margin"></div>
            <div className="column content">
                <div className="logo-container">
                    <header>Sznuq</header>
                    <img src={require("../../assets/logoX.png")}/>
                </div>
                <div className="form-container">
                    <div className='state-buttons'>
                        <button name='login' className='selected' onClick={handleStateButtonClick}>Login</button>
                        <button name='register' onClick={handleStateButtonClick}>Register</button>
                    </div>
                    <div className='state-form'>
                        {state === 'login'
                            ? <LoginForm/>
                            : <RegisterForm/>
                        }
                    </div>
                </div>
            </div>
            <div className="column margin"></div>
        </div>
    )
}