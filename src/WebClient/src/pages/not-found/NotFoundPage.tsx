import { useNavigate } from 'react-router-dom';
import './NotFoundPage.css';

export default function NotFoundPage() {
    const navigate = useNavigate();
    const handleGoHomeClick = () => navigate('/');

    return (
        <div className="app-page not-found">
            <header>Not found</header>
            <a onClick={handleGoHomeClick}>Go Home page</a>
        </div>
    )
}