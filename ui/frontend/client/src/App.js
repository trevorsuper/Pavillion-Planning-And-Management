// App.js
import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import './css/Home.css';

import Header from './components/Header'; // assuming you've created it
import Contact from './html/Contact.js';
import Explore from './html/Explore.js';
import ParkEvents from './html/ParkEvents.js';
import FAQ from './html/FAQ.js';
import Book from './html/book.js';
import Login from './html/login.js';
import Home from './html/Home.js'; // ✅ import the real Home component

function App() {
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetch('http://localhost:5000/api/hello')
      .then((res) => res.json())
      .then((data) => setMessage(data.message));
  }, []);

  return (
    <Router>
      <Routes>
        {/* ✅ Homepage now uses the Home.js component */}
        <Route path="/" element={<Home />} />

        {/* ✅ Redirect /home to / to avoid duplication */}
        <Route path="/home" element={<Navigate to="/" />} />

        <Route path="/events" element={<ParkEvents />} />
        <Route path="/explore" element={<Explore />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/faq" element={<FAQ />} />
        <Route path="/book" element={<Book />} />
        <Route path="/login" element={<Login />} />
      </Routes>

      {/* Optional server message display */}
      <section style={{ padding: '20px', textAlign: 'center', fontSize: '1.25rem' }}>
        <strong>Server Message:</strong> {message}
      </section>
    </Router>
  );
}

export default App;
