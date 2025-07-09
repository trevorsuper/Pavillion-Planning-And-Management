import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import '../css/Home.css';

const images = [
  'images/boulan-park.jpg',
  'images/brinston-park.jpg',
  'images/firefighters-park.jpg',
  'images/jaycee-park.jpg',
  'images/milverton-park.jpg',
  'images/raintree-park.jpg',
  'images/jeanne-stine-community-park.jpg',
];

const Home = () => {
  const [index, setIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setIndex((prev) => (prev + 1) % images.length);
    }, 3000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div>
      <header className="header">
        <Link to="/home">
          <img src="images/Troy_Homepage.png" alt="Troy Michigan Logo" className="logo" />
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
          <Link to="https://www.facebook.com/TroyMI/"><img src="images/facebook-icon.png" alt="Facebook" /></Link>
          <Link to="https://x.com/CityTroyMI"><img src="images/twitter-icon.png" alt="Twitter" /></Link>
          <Link to="https://www.instagram.com/troymichigan/"><img src="images/instagram-icon.png" alt="Instagram" /></Link>
          <Link to="https://www.youtube.com/TroyMichiganGov"><img src="images/youtube-icon.png" alt="YouTube" /></Link>
        </div>
      </header>

      <section className="hero">
        <div className="carousel">
          {images.map((src, i) => (
            <img
              key={i}
              src={src}
              alt={`Slide ${i + 1}`}
              className={i === index ? 'active' : ''}
            />
          ))}
        </div>
      </section>
    </div>
  );
};

export default Home;
