import React, { useEffect, useState } from 'react';
import Explore from './html/Explore.js';
import Contact from './html/Contact.js';
import Home from './html/Home.js';

function App() {
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetch('http://localhost:5000/api/hello')
      .then(res => res.json())
      .then(data => setMessage(data.message));
  }, []);

  const [currentPage, setCurrentPage] = useState('Home');

  const renderPage = () => {
    switch(currentPage) {
      case 'Home':
        return <Home />;
      case 'Explore':
        return <Explore />;
      case 'Contact':
        return <Contact />;
      default:  /* If on an unknown page, display this */
        return (
        <div>
          <h1>404 Not Found</h1>
        </div>
        );
    }
  };

  return <div>{/*{message}*/}
  <header>
    <image src="" href="#Home" alt="Logo for the city of Troy, Michigan." onClick={(e) => { e.preventDefault(); setCurrentPage('Home'); }}></image>
    <nav class="nav">
      <ul>
        <li><a href="#Book"  onClick={(e) => { e.preventDefault(); setCurrentPage('Book'); }}>Book Now!</a></li>
        <li><a href="#Events"  onClick={(e) => { e.preventDefault(); setCurrentPage('Events'); }}>Community Park Events</a></li>
        <li><a href="#Explore" onClick={(e) => { e.preventDefault(); setCurrentPage('Explore'); }}>Explore the Parks</a></li>
        <li><a href="#FAQ" onClick={(e) => { e.preventDefault(); setCurrentPage('FAQ'); }}>FAQ</a></li>
        <li><a href="#Contact" onClick={(e) => { e.preventDefault(); setCurrentPage('Contact'); }}>Contact Us</a></li>
        <li><a href="#Login" onClick={(e) => { e.preventDefault(); setCurrentPage('Login'); }}>Login</a></li>
        <li><a href="https://www.facebook.com/TroyMI/">Facebook</a></li>
        <li><a href="https://x.com/CityTroyMI">Twitter</a></li>
        <li><a href="https://www.instagram.com/troymichigan/">Instagram</a></li>
        <li><a href="https://www.youtube.com/c/TroyMichiganGov/videos">Youtube</a></li>
        <li><a href="https://nextdoor.com/city/troy--mi/">Nextdoor</a></li>
      </ul>
    </nav>
  </header>


  {renderPage()}

  </div>;
}

export default App;
