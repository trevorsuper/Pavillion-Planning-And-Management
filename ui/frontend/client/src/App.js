import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './css/Home.css';
import Contact from './html/Contact.js';
import Explore from './html/Explore.js';
import ParkEvents from './html/ParkEvents.js';
import FAQ from './html/FAQ.js';
import Book from './html/book.js';
import Login from './html/login.js';

const images = [
  'images/boulan-park.jpg',
  'images/brinston-park.jpg',
  'images/firefighters-park.jpg',
  'images/jaycee-park.jpg',
  'images/milverton-park.jpg',
  'images/raintree-park.jpg',
  'images/jeanne-stine-community-park.jpg',
];

function App() {
  const [index, setIndex] = useState(0);
  const [message, setMessage] = useState('');

  useEffect(() => {
    const interval = setInterval(() => {
      setIndex((prev) => (prev + 1) % images.length);
    }, 3000);
    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    fetch('http://localhost:5000/api/hello')
      .then((res) => res.json())
      .then((data) => setMessage(data.message));
  }, []);

  return (
    <Router>
      <header className="header">
        <Link to="/home"><img src="images/Troy_Homepage.png" alt="Troy Michigan Logo" className="logo" /></Link>
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

      <Routes>
        <Route
          path="/"
          element={
            <>
              <section className="hero">
                <div className="carousel">
                  {images.map((src, i) => (
                    <img
                      key={i}
                      src={src}
                      alt={`Slide ${i}`}
                      className={i === index ? 'active' : ''}
                    />
                  ))}
                </div>
              </section>

              <section style={{ padding: '20px', textAlign: 'center', fontSize: '1.25rem' }}>
                <strong>Server Message:</strong> {message}
              </section>
            </>
          }
        />
        <Route path="/events" element={<ParkEvents />} />
        <Route path="/explore" element={<Explore />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/faq" element={<FAQ />} />
        <Route path="/book" element={<Book />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </Router>
  );
}

export default App;
