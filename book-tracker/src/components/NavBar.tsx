import { Link, NavLink } from "react-router-dom";
import "./NavBar.css";

function NavBar() {
    return (
        <header className="navbar">
            <nav className="navbar-inner">
                <Link to="/" className="navbar-brand">BookTracker</Link>

                <div className="navbar-links">
                    <NavLink to="/" end className="navbar-link">Books</NavLink>
                    <NavLink to="/about" className="navbar-link">About</NavLink>
                </div>
            </nav>
        </header>
    );
}

export default NavBar;