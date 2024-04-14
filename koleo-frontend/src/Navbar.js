import { faHome, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";

const Navbar = () => {
  return (
    <nav className="navbar">
      <h1></h1>
      <div className="links">
        <Link to="/" className="link"> <FontAwesomeIcon icon={faHome}/> </Link>
        <Link to="/account" className="link"><FontAwesomeIcon icon={faUser}/></Link>
      </div>
    </nav>
  );
}
 
export default Navbar;