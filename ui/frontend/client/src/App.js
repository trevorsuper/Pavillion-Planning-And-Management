// App.js
import React, { useEffect, useState } from 'react';
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from 'react-router-dom';
import './css/Home.css';

import { AuthProvider } from './context/AuthContext';
import AdminRoute from './components/AdminRoute';

import AdminDashboard from './html/AdminDashboard';
import Contact from './html/Contact.js';
import Explore from './html/Explore.js';
import ParkEvents from './html/ParkEvents.js';
import FAQ from './html/FAQ.js';
import Book from './html/book.js';
import Login from './html/login.js';
import Home from './html/Home.js';
import MyBookings from './html/MyBookings';

function App() {
  const [message, setMessage] = useState('');

  useEffect(() => {
    fetch('http://localhost:5000/api/hello')
      .then((res) => res.json())
      .then((data) => setMessage(data.message))
      .catch(() => {
        // optional: swallow or log
      });
  }, []);

  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Navigate to="/" replace />} />
          <Route path="/my-bookings" element={<MyBookings />} />
          <Route path="/events" element={<ParkEvents />} />
          <Route path="/explore" element={<Explore />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/faq" element={<FAQ />} />
          <Route path="/book" element={<Book />} />
          <Route path="/login" element={<Login />} />

          <Route
            path="/admin"
            element={
              <AdminRoute>
                <AdminDashboard />
              </AdminRoute>
            }
          />

          {/* catch-all fallback */}
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;

