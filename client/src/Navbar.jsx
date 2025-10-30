import { faHome, faPowerOff, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, useNavigate } from "react-router-dom";

const Navbar = () => {
    const navigate = useNavigate();
    const login = window.localStorage.getItem("isLoggedIn");

    const handleLogout = () => {

        localStorage.clear('id');
        localStorage.clear('jwtToken');
        console.log('logged out');
    };

    return (
        <nav className="navbar">
            <h1></h1>
            <div className="links">

                {/* {localStorage.get('id') ? <Link to="/login" className="link" onClick={handleLogout}><FontAwesomeIcon icon={faPowerOff}/></Link> : null} */}
                {/* {login ? <button className="log-out-button" onClick={handleLogout}>Wyloguj się</button> : <></> } */}
                {
                    localStorage.getItem('id') ?
                        <>

                            <Link to="/home" className="link"> <FontAwesomeIcon icon={faHome} /> </Link>
                            <Link to="/account" className="link"><FontAwesomeIcon icon={faUser} /></Link>
                            <Link to="/login" className="link" onClick={handleLogout}><FontAwesomeIcon icon={faPowerOff} /></Link>
                        </>
                        : null
                }
                {/* <Link to="/login" className="link">Wyloguj się</Link> */}
                {/* {login ? <button className="log-out-button" onClick={handleLogout}>Wyloguj się</button> : <></> } */}
            </div>
        </nav>
    );
}

export default Navbar;