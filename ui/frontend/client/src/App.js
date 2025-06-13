import React, { useEffect, useState } from 'react';

function App() {
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetch('http://localhost:5000/api/hello')
      .then(res => res.json())
      .then(data => setMessage(data.message));
  }, []);

  return <div>{/*{message}*/}
  <header>
    <image src="" alt="Logo for the city of Troy, Michigan."></image>
    <nav class="nav">
      <ul>
        <li><a href="">Book Now!</a></li>
        <li><a href="">Community Park Events</a></li>
        <li><a href="">Explore the Parks</a></li>
        <li><a href="">FAQ</a></li>
        <li><a href="">Contact Us</a></li>
        <li><a href="">Login</a></li>
        <li><a href="facebook">Facebook</a></li>
        <li><a href="twitter">Twitter</a></li>
        <li><a href="instagram">Instagram</a></li>
        <li><a href="youtube">Youtube</a></li>
        <li><a href="nextdoor">Nextdoor</a></li>
      </ul>
    </nav>
  </header>
  
  <main>
    <image src="" alt="Image carousel of different parks in Troy."></image>
  </main>


  </div>;
}

export default App;
