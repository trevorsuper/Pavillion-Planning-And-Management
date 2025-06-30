// src/components/Header.js
import React from 'react';
import { Link } from 'react-router-dom';
import '../css/Home.css'; // Adjust this if your nav styles are elsewhere

const Header = () => {
  return (
    <header className="header">
      <Link to="/home">
        <img
          src="images/Troy_Homepage.png"
          alt="Troy Michigan Logo"
          className="logo"
        />
      </Link>

      <nav>
        <Link to="/book">Book Now!</Link>
        <Link to="/events">Community Park Events</Link>
        <Link to="/explore">Explore the Parks</Link>
        <Link to="/faq">FAQ</Link>
        <Link to="/contact">Contact us</Link>
        <Link to="/login">Sign up/Login</Link>
      </nav>

      <div className="socials">
        <img src="images/facebook-icon.png" alt="Facebook" />
        <img src="images/twitter-icon.png" alt="Twitter" />
        <img src="images/instagram-icon.png" alt="Instagram" />
        <img src="images/youtube-icon.png" alt="YouTube" />
      </div>
    </header>
  );
};

export default Header;
