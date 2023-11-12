import { useState } from "react";
import Navbar from "./Navbar";
import { useStore } from "../../stores/store";
import { useNavigate } from "react-router-dom";
import QuizzesList from "./QuizzesList";
import SessionsList from "./SessionsList";
import './HomePage.css';

export default function HomePage() {
    const {userStore} = useStore();
    const navigate = useNavigate();
    const [menuState, setMenuState] = useState('quizzes');

    if(menuState === 'logout') userStore.logout().then(() => navigate('/'));

    return (
        <div className="app-page home">
            <Navbar setMenuState={setMenuState} menuState={menuState}/>
            <main>
                {menuState === 'quizzes'
                    ? <QuizzesList />
                    : <SessionsList />
                }
            </main>
        </div>
    )
}