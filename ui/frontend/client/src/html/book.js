import React, { useState, useEffect } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import '../css/book.css';
import Header from '../components/Header';
import { useAuth } from '../context/AuthContext';

const fallbackParkNames = [
  'Boulan Park',
  'Brinston Park',
  'Firefighters Park',
  'Jaycee Park',
  'Milverton Park',
  'Raintree Park',
  'Jeanne M Stine Community Park',
];

const timeSlots = [
  { label: 'Full Day (9 AM–5 PM)', start: '09:00', end: '17:00' },
  { label: '9 AM–12 PM', start: '09:00', end: '12:00' },
  { label: '12 PM–3 PM', start: '12:00', end: '15:00' },
  { label: '3 PM–6 PM', start: '15:00', end: '18:00' },
];

function Book() {
  const [parks, setParks] = useState([]);
  const [selectedPark, setSelectedPark] = useState(null);
  const [selectedDate, setSelectedDate] = useState(null);
  const [selectedSlot, setSelectedSlot] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');
  const [bookedDates, setBookedDates] = useState([]);
  const [loadingParks, setLoadingParks] = useState(true);

  const { user, isAuthenticated } = useAuth();

  useEffect(() => {
    // Example of pre-blocked dates; replace with real data if needed
    setBookedDates([new Date(2025, 5, 25), new Date(2025, 5, 28)]);
  }, []);

  useEffect(() => {
    const fetchParks = async () => {
      setLoadingParks(true);
      try {
        const headers = {
          'Content-Type': 'application/json',
          ...(user?.token && { Authorization: `Bearer ${user.token}` }),
        };
        const res = await fetch('https://localhost:7203/api/Park/GetAllParks', {
          method: 'GET',
          headers,
        });
        if (res.ok) {
          const data = await res.json();
          setParks(data);
        } else {
          console.warn(
            'Failed to load parks, falling back to hardcoded list:',
            await res.text()
          );
          setParks(
            fallbackParkNames.map((name, i) => ({
              park_id: i + 1,
              park_name: name,
            }))
          );
        }
      } catch (e) {
        console.warn('Error loading parks, falling back to hardcoded list:', e);
        setParks(
          fallbackParkNames.map((name, i) => ({
            park_id: i + 1,
            park_name: name,
          }))
        );
      } finally {
        setLoadingParks(false);
      }
    };

    fetchParks();
  }, [user?.token]);

  const openBooking = (park) => {
    if (!isAuthenticated) {
      alert('You must be logged in to book a pavilion.');
      return;
    }
    setSelectedPark(park);
    setSelectedDate(null);
    setSelectedSlot(null);
    setErrorMessage('');
  };

  const closeBooking = () => {
    setSelectedPark(null);
    setSelectedDate(null);
    setSelectedSlot(null);
    setErrorMessage('');
  };

  const handleSelectSlot = (slot) => {
    setSelectedSlot(slot);
    setErrorMessage('');
  };

  const handleConfirm = async () => {
    if (!selectedPark) {
      setErrorMessage('Please select a park.');
      return;
    }
    if (!selectedDate) {
      setErrorMessage('Please select a date.');
      return;
    }
    if (!selectedSlot) {
      setErrorMessage('Please select a time slot.');
      return;
    }

    const bookingData = {
      user_id: user?.user_id,
      park_id: selectedPark.park_id,
      requested_park: selectedPark.park_name,
      registration_date: selectedDate,
      start_time: selectedSlot.start + ':00',
      end_time: selectedSlot.end + ':00',
    };

    console.log('Booking Data Being Sent:', bookingData);

    try {
      const response = await fetch('https://localhost:7203/api/Registration', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          ...(user?.token && { Authorization: `Bearer ${user.token}` }),
        },
        body: JSON.stringify(bookingData),
      });

      if (!response.ok) {
        const text = await response.text();
        setErrorMessage(`Booking failed: ${text}`);
        return;
      }

      alert(
        `Booking confirmed for ${selectedPark.park_name} on ${selectedDate.toLocaleDateString()} (${selectedSlot.label})`
      );
      closeBooking();
    } catch (error) {
      setErrorMessage(`Network error: ${error.message}`);
    }
  };

  return (
    <>
      <Header />
      <div className="p-6 max-w-4xl mx-auto">
        <h1 className="text-2xl font-bold mb-6">Book a Pavilion</h1>
        {loadingParks ? (
          <div>Loading parks...</div>
        ) : (
          <table className="w-full border-collapse">
            <thead>
              <tr className="bg-blue-100 text-left">
                <th className="p-3 border">Park</th>
                <th className="p-3 border">Action</th>
              </tr>
            </thead>
            <tbody>
              {parks.map((park, i) => (
                <tr key={i} className="bg-white hover:bg-gray-50">
                  <td className="p-3 border font-medium">{park.park_name}</td>
                  <td className="p-3 border">
                    <button
                      className="text-blue-600 hover:underline"
                      onClick={() => openBooking(park)}
                    >
                      Book now!
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>

      {selectedPark && (
        <div className="booking-popup">
          <h2 className="text-lg font-bold mb-3">
            Book {selectedPark.park_name}
          </h2>

          <p className="mb-1">
            <strong>Name:</strong>{' '}
            {user?.name ||
              `${user?.first_name || ''} ${user?.last_name || ''}`.trim()}
          </p>
          <p className="mb-4">
            <strong>Email:</strong> {user?.email}
          </p>

          <label className="block mb-4 font-medium">Select a date:</label>
          <DatePicker
            inline
            selected={selectedDate}
            onChange={setSelectedDate}
            excludeDates={bookedDates}
            minDate={new Date()}
          />

          <label className="block mb-3 font-medium">
            Choose a time slot:
          </label>
          <div className="timeslot-grid mb-4">
            {timeSlots.map((slot) => (
              <button
                key={slot.label}
                className={`timeslot-button ${
                  selectedSlot === slot ? 'selected' : ''
                }`}
                onClick={() => handleSelectSlot(slot)}
              >
                {slot.label}
              </button>
            ))}
          </div>

          {errorMessage && (
            <div className="text-red-600 mb-3">{errorMessage}</div>
          )}

          <div className="button-row">
            <button onClick={closeBooking} className="btn-cancel">
              Cancel
            </button>
            <button onClick={handleConfirm} className="btn-confirm">
              Confirm Booking
            </button>
          </div>
        </div>
      )}
    </>
  );
}

export default Book;
