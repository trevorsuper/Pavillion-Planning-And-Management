import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import '../css/Home.css';

const Home = () => {
  const images = [
    "https://metrodetroitmommy.com/familywp/wp-content/uploads/2019/05/firefighters-park-troy-4-1024x576.jpg",
    "https://metrodetroitmommy.com/familywp/wp-content/uploads/2020/06/Jaycee-Park-in-Troy-1.jpg",
    "https://patch.com/img/cdn/users/489361/2011/07/raw/27832d1f093d7de2b897cd8f1deba8e8.jpg?width=1200",
    "https://cdn.oaklandcountymoms.com/wp-content/uploads/2023/11/10115542/BoulanParkTroyHeader1-620x350.jpg",
    "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEiYyWeL1GgFSKrcoCjPLzMRLkx5um0cvUcGyUbgkuKuVR2oOq927kQstRq5TmwkVMxOpA7i8mVjgJSUFpz4H7EF_afhB8ALrCCADI1WwMuDuv8X3Z_6HBhWjoTNUraGtTIcl-2_i63D039j/s320/milverton+park+gifted+to+Palmerston+north+by+milvertons.JPG",
    "https://metrodetroitmommy.com/familywp/wp-content/uploads/2018/05/Untitled6.gif",
    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fres.cloudinary.com%2Fdgbcm65u4%2Fimage%2Fupload%2Fc_fill%2Ce_sharpen%3A100%2Cg_auto%2Ch_585%2Cw_1069%2Flkevultljxyvh5nhthyr&f=1&nofb=1&ipt=2ab141f7b055988719466d5d50496fa6743e02fa71cf33383668c0b9eff7a2c9",
  ]

  const [imgIndex, setImgIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setImgIndex((prevImgIndex) =>
      prevImgIndex === images.length - 1 ? 0 : prevImgIndex + 1);
    }, 5000);    //5000ms

    return () => clearInterval(interval);
  }, [images.length]);

  const nextImage = () => {
    setImgIndex(imgIndex === images.length - 1 ? 0 : imgIndex + 1);
  }

  const prevImage = () => {
    setImgIndex(imgIndex === 0 ? images.length - 1 : imgIndex - 1);
  }

  const goToImage = (index) => {
    setImgIndex(index);
  }

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
          <a href="https://www.facebook.com/TroyMI/"><img src="images/facebook-icon.png" alt="Facebook" /></a>
          <a href="https://x.com/CityTroyMI"><img src="images/twitter-icon.png" alt="Twitter" /></a>
          <a href="https://www.instagram.com/troymichigan/"><img src="images/instagram-icon.png" alt="Instagram" /></a>
          <a href="https://www.youtube.com/TroyMichiganGov"><img src="images/youtube-icon.png" alt="YouTube" /></a>
        </div>
      </header>

      <section className="hero">
        <div className="carousel-container">
          <img src={images[imgIndex]} alt="Image carousel of different parks in Troy." />
        </div>
      </section>
    </div>
  );
};

export default Home;
