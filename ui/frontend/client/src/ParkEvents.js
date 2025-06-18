import React from 'react';
import './ParkEvents.css';

const events = [
  {
    title: 'Disc Golf Tournament',
    date: 'July 15, 2025',
    spots: 25,
  },
  {
    title: 'Community Easter Egg Hunt',
    date: 'April 12, 2025',
    spots: 100,
  },
  {
    title: 'Northern Lights Show',
    date: 'December 2, 2025',
    spots: 200,
  },
];

const ParkEvents = () => {
  return (
    <div className="events-container">
      <h1 className="events-title">Upcoming Registerable Park Events</h1>
      <div className="event-list">
        {events.map((event, idx) => (
          <div key={idx} className="event-card">
            <h2>{event.title}</h2>
            <p><strong>Date:</strong> {event.date}</p>
            <p><strong>Spots Available:</strong> {event.spots}</p>
            <button className="register-btn">Register</button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ParkEvents;
