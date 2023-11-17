import './Navbar.css';

interface Props {
    setMenuState: (state: string) => void;
    menuState: string;
}

export default function Navbar({setMenuState, menuState}: Props) {
    const handleMenuElemClick = (evnt: React.MouseEvent<HTMLDivElement>) => {
        const element = evnt.currentTarget;
        const option = element.id;
        switch(option)
        {
            case 'quizzes':
                setMenuState('quizzes');
                break;
            case 'sessions':
                setMenuState('sessions');
                break;
            case 'logout':
                setMenuState('logout');
                break;
        }
    }

    const menuOptions = [
        {name: 'Quizzes', id: 'quizzes', imgSrc: require('../../assets/quiz-white.png')},
        {name: 'Sessions', id: 'sessions', imgSrc: require('../../assets/session-white.png')},
        {name: 'Logout', id: 'logout', imgSrc: require('../../assets/logout-white.png')}
    ]

    return (
        <nav>
            <div id='menu'>
                {menuOptions.map(option => 
                    <div id={option.id} className={menuState === option.id ? 'menu-elem active' : 'menu-elem'} onClick={handleMenuElemClick} key={option.id}>
                        <img src={option.imgSrc}/>
                        {option.name}
                    </div>
                )}
            </div>
        </nav>
    )
}