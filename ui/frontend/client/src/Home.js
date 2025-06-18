import React, { useEffect, useState } from 'react';
import './Home.css';

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
        <img src="images/Troy_Homepage.png" alt="Troy Michigan Logo" className="logo" />
        <nav>
          <a href="#book">Book Now!</a>
          <a href="#events">Community Park Events</a>
          <a href="#explore">Explore the Parks</a>
          <a href="#faq">FAQ</a>
          <a href="#contact">Contact an employee</a>
          <a href="#login">Sign up/Login</a>
        </nav>
        <div className="socials">
          <img src="images/facebook-icon.png" alt="Facebook" />
          <img src="images/twitter-icon.png" alt="Twitter" />
          <img src="images/instagram-icon.png" alt="Instagram" />
          <img src="images/youtube-icon.png" alt="YouTube" />
        </div>
      </header>

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
    </div>
  );
};

export default Home;
