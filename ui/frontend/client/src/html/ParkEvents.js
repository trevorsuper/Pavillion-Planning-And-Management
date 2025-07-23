import React, { useState } from 'react';
import '../css/ParkEvents.css';
import Header from '../components/Header';
import { useAuth } from '../context/AuthContext';

const events = [
  {
    id: 1,
    title: 'Disc Golf Tournament',
    date: 'July 15, 2025',
    spots: 25,
  },
  {
    id: 2,
    title: 'Community Easter Egg Hunt',
    date: 'April 12, 2025',
    spots: 100,
  },
  {
    id: 3,
    title: 'Northern Lights Show',
    date: 'December 2, 2025',
    spots: 200,
  },
];

const ParkEvents = () => {
  const { user, isAuthenticated } = useAuth();
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [reservationName, setReservationName] = useState('');
  const [partySize, setPartySize] = useState('');
  const [userReservations, setUserReservations] = useState([]); // { eventId, name, size }

  const handleRegisterClick = (event) => {
    setSelectedEvent(event);

    // Prefill form if already registered
    const existing = userReservations.find(r => r.eventId === event.id);
    if (existing) {
      setReservationName(existing.name);
      setPartySize(existing.size);
    } else {
      setReservationName('');
      setPartySize('');
    }
  };

  const closeModal = () => {
    setSelectedEvent(null);
    setReservationName('');
    setPartySize('');
  };

  const handleReservationSubmit = (e) => {
    e.preventDefault();

    // --- ⬇️ Backend API call to save reservation ---
    /*
    fetch('/api/reservations', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        eventId: selectedEvent.id,
        name: reservationName,
        size: partySize,
        userId: loggedInUser.id // if you have a user object
      })
    }).then(res => res.json())
      .then(data => {
        // Handle success
      }).catch(err => {
        console.error("Error saving reservation", err);
      });
    */

    // Local mock update
    const updated = [...userReservations.filter(r => r.eventId !== selectedEvent.id)];
    updated.push({
      eventId: selectedEvent.id,
      name: reservationName,
      size: partySize,
    });
    setUserReservations(updated);
    closeModal();
  };

  const handleCancelReservation = () => {
    // --- ⬇️ Backend API call to delete reservation ---
    /*
    fetch(`/api/reservations/${selectedEvent.id}`, {
      method: 'DELETE',
      headers: { 'Authorization': `Bearer ${token}` }
    }).then(() => {
      // Handle success
    }).catch(err => {
      console.error("Error cancelling reservation", err);
    });
    */

    // Local mock removal
    setUserReservations(userReservations.filter(r => r.eventId !== selectedEvent.id));
    closeModal();
  };

  const isRegistered = selectedEvent
    ? userReservations.some(r => r.eventId === selectedEvent.id)
    : false;

  return (
    <>
      <Header />
      <div className="events-container">
        <h1 className="events-title">Upcoming Registerable Park Events</h1>
        <div className="event-list">
          {events.map((event) => (
            <div key={event.id} className="event-card">
              <h2>{event.title}</h2>
              <p><strong>Date:</strong> {event.date}</p>
              <p><strong>Spots Available:</strong> {event.spots}</p>
              <button className="register-btn" onClick={() => handleRegisterClick(event)}>
                Register
              </button>
            </div>
          ))}
        </div>
      </div>

      {/* Modal */}
      {selectedEvent && (
        <div className="modal-overlay">
          <div className="modal-content">
            <button className="modal-close" onClick={closeModal}>×</button>

            <h2>{selectedEvent.title}</h2>
            <p><strong>Date:</strong> {selectedEvent.date}</p>

            {!isAuthenticated ? (
              <div>
                <p>You must be logged in to register for this event.</p>
                {/* TODO: Replace with your actual routes */}
                <a href="/login">Login</a> or <a href="/signup">Sign up</a> to continue.
              </div>
            ) : isRegistered ? (
              <div>
                <p>You have already registered for this event.</p>
                <form onSubmit={handleReservationSubmit}>
                  <label>
                    Update Reservation Name:
                    <input
                      type="text"
                      value={reservationName}
                      onChange={(e) => setReservationName(e.target.value)}
                      required
                    />
                  </label>
                  <label>
                    Update Party Size:
                    <input
                      type="number"
                      value={partySize}
                      onChange={(e) => setPartySize(e.target.value)}
                      min="1"
                      required
                    />
                  </label>
                  <button type="submit">Update Reservation</button>
                  <button
                    type="button"
                    onClick={handleCancelReservation}
                    style={{ backgroundColor: '#bb0000', marginTop: '0.5rem', color: 'white' }}
                  >
                    Cancel Reservation
                  </button>
                </form>
              </div>
            ) : (
              <form onSubmit={handleReservationSubmit}>
                <label>
                  Reservation Name:
                  <input
                    type="text"
                    value={reservationName}
                    onChange={(e) => setReservationName(e.target.value)}
                    required
                  />
                </label>
                <label>
                  Party Size:
                  <input
                    type="number"
                    value={partySize}
                    onChange={(e) => setPartySize(e.target.value)}
                    min="1"
                    required
                  />
                </label>
                <button type="submit">Submit Reservation</button>
              </form>
            )}
          </div>
        </div>
      )}
    </>
  );
};

export default ParkEvents;
