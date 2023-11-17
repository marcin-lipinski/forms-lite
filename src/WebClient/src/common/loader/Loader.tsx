import './Loader.css';

interface Props {
    active?: boolean;
}

export default function Laoder({active}: Props) {
    if(active === undefined || active === true){
        return(
            <div id="loader-background">
                <div id="loader-spiral"></div>
            </div>
        )
    }  
    return(<></>);
}