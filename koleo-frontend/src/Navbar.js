import { faHome, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, useNavigate } from "react-router-dom";

const Navbar = () => {
    const navigate = useNavigate();
    const login = window.localStorage.getItem("isLoggedIn");

    const handleLogout = () => {
        // e.preventDefault();
        window.localStorage.setItem("isLoggedIn", false);
        console.log('logged out');
        navigate("/")
      };

    return (
        <nav className="navbar">
        <h1></h1>
        <div className="links">
            <Link to="/" className="link"> <FontAwesomeIcon icon={faHome}/> </Link>
            <Link to="/account" className="link"><FontAwesomeIcon icon={faUser}/></Link>
            {/* <Link to="/login" className="link">Wyloguj się</Link> */}
            {login ? <button className="log-out-button" onClick={handleLogout}>Wyloguj się</button> : <></> }
        </div>
        </nav>
    );
}
 
export default Navbar;