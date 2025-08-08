// src/components/Header.js
import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../css/Home.css';

const Header = () => {
  const { user, isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  const closeMobileMenu = () => {
    setMobileMenuOpen(false);
  };

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
      <div className="header-top">
        <Link to="/home" className="nav-link logo-link">
          <img src="images/Troy_Homepage.png" alt="Troy Michigan Logo" className="logo" />
        </Link>

        {/* Mobile menu toggle button */}
        <button 
          className="mobile-menu-toggle"
          onClick={toggleMobileMenu}
          aria-label="Toggle mobile menu"
        >
          ☰
        </button>
      </div>

      {/* Desktop/Tablet Navigation */}
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
        <a href="https://www.facebook.com/TroyMI/" target="_blank" rel="noopener noreferrer">
          <img src="images/facebook-icon.png" alt="Facebook" />
        </a>
        <a href="https://x.com/CityTroyMI" target="_blank" rel="noopener noreferrer">
          <img src="images/twitter-icon.png" alt="Twitter" />
        </a>
        <a href="https://www.instagram.com/troymichigan/" target="_blank" rel="noopener noreferrer">
          <img src="images/instagram-icon.png" alt="Instagram" />
        </a>
        <a href="https://www.youtube.com/TroyMichiganGov" target="_blank" rel="noopener noreferrer">
          <img src="images/youtube-icon.png" alt="YouTube" />
        </a>
      </div>

      {/* Mobile Navigation */}
      <nav className={`mobile-nav ${mobileMenuOpen ? 'active' : ''}`}>
        <Link to="/book" onClick={closeMobileMenu}>Book Now!</Link>
        <Link to="/events" onClick={closeMobileMenu}>Community Park Events</Link>
        <Link to="/explore" onClick={closeMobileMenu}>Explore the Parks</Link>
        <Link to="/faq" onClick={closeMobileMenu}>FAQ</Link>
        <Link to="/contact" onClick={closeMobileMenu}>Contact us</Link>

        {isAuthenticated && (
          <Link to="/my-bookings" onClick={closeMobileMenu}>My Bookings</Link>
        )}

        {admin && (
          <Link to="/admin" onClick={closeMobileMenu}>View Registration Requests</Link>
        )}

        {isAuthenticated ? (
          <button onClick={() => { handleLogout(); closeMobileMenu(); }} className="mobile-logout-button">
            Logout
          </button>
        ) : (
          <Link to="/login" onClick={closeMobileMenu}>Sign up/Login</Link>
        )}
        
        {/* Social media row in mobile menu */}
        <div className="mobile-socials">
          <a href="https://www.facebook.com/TroyMI/" target="_blank" rel="noopener noreferrer">
            <img src="images/facebook-icon.png" alt="Facebook" />
          </a>
          <a href="https://x.com/CityTroyMI" target="_blank" rel="noopener noreferrer">
            <img src="images/twitter-icon.png" alt="Twitter" />
          </a>
          <a href="https://www.instagram.com/troymichigan/" target="_blank" rel="noopener noreferrer">
            <img src="images/instagram-icon.png" alt="Instagram" />
          </a>
          <a href="https://www.youtube.com/TroyMichiganGov" target="_blank" rel="noopener noreferrer">
            <img src="images/youtube-icon.png" alt="YouTube" />
          </a>
        </div>
      </nav>
    </header>
  );
};

export default Header;