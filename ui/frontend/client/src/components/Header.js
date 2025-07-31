// src/components/Header.js
import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../css/Home.css';

const Header = () => {
  const { user, isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = async () => {
    try {
      await logout();
    } catch (e) {
      console.warn('Logout error', e);
    }
    navigate('/home', { replace: true });
  };

  const isAdmin = (u) => {
    if (!u) return false;
    if (u.is_admin === true || u.isAdmin === true) return true;
    if (u.is_admin === 'true' || u.isAdmin === 'true') return true;
    if (u.is_admin === 1 || u.isAdmin === 1) return true;
    if (u.is_admin === '1' || u.isAdmin === '1') return true;
    return false;
  };

  const admin = isAdmin(user);

  return (
    <header className="header">
      <Link to="/home" className="nav-link logo-link">
        <img src="images/Troy_Homepage.png" alt="Troy Michigan Logo" className="logo" />
      </Link>

      <nav>
        <Link to="/book" className="nav-link">
          Book Now!
        </Link>
        <Link to="/events" className="nav-link">
          Community Park Events
        </Link>
        <Link to="/explore" className="nav-link">
          Explore the Parks
        </Link>
        <Link to="/faq" className="nav-link">
          FAQ
        </Link>
        <Link to="/contact" className="nav-link">
          Contact us
        </Link>

        {isAuthenticated && (
          <Link to="/my-bookings" className="nav-link">
            My Bookings
          </Link>
        )}

        {admin && (
          <Link to="/admin" className="nav-link registration-requests" style={{ marginLeft: '8px' }}>
            View Registration Requests
          </Link>

        )}

        {isAuthenticated ? (
          <button onClick={handleLogout} className="nav-link logout-button" aria-label="Logout" type="button">
            Logout
          </button>
        ) : (
          <Link to="/login" className="nav-link">
            Sign up/Login
          </Link>
        )}
      </nav>

      <div className="socials">
        <a href="https://www.facebook.com/TroyMI/">
          <img src="images/facebook-icon.png" alt="Facebook" />
        </a>
        <a href="https://x.com/CityTroyMI">
          <img src="images/twitter-icon.png" alt="Twitter" />
        </a>
        <a href="https://www.instagram.com/troymichigan/">
          <img src="images/instagram-icon.png" alt="Instagram" />
        </a>
        <a href="https://www.youtube.com/TroyMichiganGov">
          <img src="images/youtube-icon.png" alt="YouTube" />
        </a>
      </div>
    </header>
  );
};

export default Header;
