// Book.js
import React, { useState } from 'react';
import '../css/book.css';
import Header from '../components/Header';

const mockEventData = [
  {
    id: 1,
    name: 'Music in the Park',
    timeslots: [
      { id: 'a', date: 'June 20, 2025', time: '6:00 PM', available: true },
      { id: 'b', date: 'June 21, 2025', time: '6:00 PM', available: false },
    ],
  },
  {
    id: 2,
    name: 'Farmers Market',
    timeslots: [
      { id: 'c', date: 'June 22, 2025', time: '10:00 AM', available: true },
      { id: 'd', date: 'June 23, 2025', time: '10:00 AM', available: true },
    ],
  },
];

function Book() {
  const [expandedEvent, setExpandedEvent] = useState(null);

  const toggleExpand = (eventId) => {
    setExpandedEvent((prev) => (prev === eventId ? null : eventId));
  };

  return (
    <>
      <Header />
      <div className="p-6 max-w-4xl mx-auto">
        <h1 className="text-2xl font-bold mb-6">Book Now</h1>
        <table className="w-full border-collapse">
          <thead>
            <tr className="bg-blue-100 text-left">
              <th className="p-3 border">Event</th>
              <th className="p-3 border">Action</th>
            </tr>
          </thead>
          <tbody>
            {mockEventData.map((event) => (
              <React.Fragment key={event.id}>
                <tr className="bg-white hover:bg-gray-50">
                  <td className="p-3 border font-medium">{event.name}</td>
                  <td className="p-3 border">
                    <button
                      className="text-blue-600 hover:underline"
                      onClick={() => toggleExpand(event.id)}
                    >
                      {expandedEvent === event.id ? 'Hide Times' : 'View Times'}
                    </button>
                  </td>
                </tr>

                {expandedEvent === event.id && (
                  <tr>
                    <td colSpan="2" className="p-3 border bg-gray-50">
                      <ul className="space-y-2">
                        {event.timeslots.map((slot) => (
                          <li
                            key={slot.id}
                            className="flex justify-between items-center border-b pb-2"
                          >
                            <div>
                              <span className="font-semibold">{slot.date}</span> at {slot.time}
                            </div>
                            {slot.available ? (
                              <button className="bg-blue-600 text-white px-4 py-1 rounded hover:bg-blue-500">
                                Reserve
                              </button>
                            ) : (
                              <span className="text-red-600 font-medium">Full</span>
                            )}
                          </li>
                        ))}
                      </ul>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default Book;
