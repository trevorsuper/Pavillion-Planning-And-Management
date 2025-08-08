// src/components/Header.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../css/Home.css';

const Header = () => {
  const { user, isAuthenticated } = useAuth();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  const closeMobileMenu = () => {
    setMobileMenuOpen(false);
  };

  return (
    <header className="header">
      <div className="header-top">
        <Link to="/home">
          <img
            src="images/Troy_Homepage.png"
            alt="Troy Michigan Logo"
            className="logo"
          />
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
        <Link to="/book">Book Now!</Link>
        <Link to="/events">Community Park Events</Link>
        <Link to="/explore">Explore the Parks</Link>
        <Link to="/faq">FAQ</Link>
        <Link to="/contact">Contact us</Link>

        {isAuthenticated && (
          <Link to="/my-bookings">My Bookings</Link>
        )}

        <Link to="/login">Sign up/Login</Link>
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

        <Link to="/login" onClick={closeMobileMenu}>Sign up/Login</Link>
        
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