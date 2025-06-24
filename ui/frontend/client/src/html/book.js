// Book.js
import React, { useState } from 'react';
import '../css/book.css';
import Header from '../components/Header';

const parkData = [
  {
    id: 1,
    name: 'Boulan Park',
    timeslots: [
      { id: 'bp1', date: 'June 25, 2025', time: '10:00 AM', available: true },
      { id: 'bp2', date: 'June 25, 2025', time: '2:00 PM', available: false },
    ],
  },
  {
    id: 2,
    name: 'Brinston Park',
    timeslots: [
      { id: 'br1', date: 'June 26, 2025', time: '11:00 AM', available: true },
    ],
  },
  {
    id: 3,
    name: 'Firefighters Park',
    timeslots: [
      { id: 'fp1', date: 'June 27, 2025', time: '1:00 PM', available: true },
    ],
  },
  {
    id: 4,
    name: 'Jaycee Park',
    timeslots: [
      { id: 'jp1', date: 'June 28, 2025', time: '9:00 AM', available: false },
      { id: 'jp2', date: 'June 28, 2025', time: '3:00 PM', available: true },
    ],
  },
  {
    id: 5,
    name: 'Milverton Park',
    timeslots: [
      { id: 'mp1', date: 'June 29, 2025', time: '10:30 AM', available: true },
    ],
  },
  {
    id: 6,
    name: 'Raintree Park',
    timeslots: [
      { id: 'rp1', date: 'June 30, 2025', time: '12:00 PM', available: true },
    ],
  },
  {
    id: 7,
    name: 'Jeanne M Stine Community Park',
    timeslots: [
      { id: 'js1', date: 'July 1, 2025', time: '11:00 AM', available: false },
      { id: 'js2', date: 'July 1, 2025', time: '2:00 PM', available: true },
    ],
  },
];

function Book() {
  const [expandedPark, setExpandedPark] = useState(null);

  const toggleExpand = (parkId) => {
    setExpandedPark((prev) => (prev === parkId ? null : parkId));
  };

  return (
    <>
      <Header />
      <div className="p-6 max-w-4xl mx-auto">
        <h1 className="text-2xl font-bold mb-6">Book a Pavilion</h1>
        <table className="w-full border-collapse">
          <thead>
            <tr className="bg-blue-100 text-left">
              <th className="p-3 border">Park</th>
              <th className="p-3 border">Action</th>
            </tr>
          </thead>
          <tbody>
            {parkData.map((park) => (
              <React.Fragment key={park.id}>
                <tr className="bg-white hover:bg-gray-50">
                  <td className="p-3 border font-medium">{park.name}</td>
                  <td className="p-3 border">
                    <button
                      className="text-blue-600 hover:underline"
                      onClick={() => toggleExpand(park.id)}
                    >
                      {expandedPark === park.id ? 'Hide Times' : 'View Times'}
                    </button>
                  </td>
                </tr>

                {expandedPark === park.id && (
                  <tr>
                    <td colSpan="2" className="p-3 border bg-gray-50">
                      <ul className="space-y-2">
                        {park.timeslots.map((slot) => (
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
