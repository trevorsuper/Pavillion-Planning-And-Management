// src/components/AdminDashboard.js
import React, { useEffect, useState } from 'react';
import Header from '../components/Header';
import { useAuth } from '../context/AuthContext';

const AdminDashboard = () => {
  const { user, isAuthenticated } = useAuth();
  const [registrations, setRegistrations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const authHeader = user?.token ? { Authorization: `Bearer ${user.token}` } : {};

  const fetchRegistrations = async () => {
    setLoading(true);
    setError('');
    try {
      const res = await fetch('/api/Registration/unreviewed', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          ...authHeader,
        },
      });

      if (!res.ok) {
        throw new Error(`Fetch failed: ${res.status}`);
      }

      const data = await res.json();
      setRegistrations(data);
    } catch (e) {
      setError(`Could not load registrations: ${e.message}`);
      setRegistrations([]);
    } finally {
      setLoading(false);
    }
  };

  const toggleApproval = async (registration) => {
    const shouldApprove = !registration.is_approved; // if currently not approved, approve
    const endpoint = shouldApprove ? `/api/Registration/${registration.registration_id}/approve` : `/api/Registration/${registration.registration_id}/reject`;
    try {
      const res = await fetch(
        endpoint,
        {
          method: 'PATCH',
          headers: {
            'Content-Type': 'application/json',
            ...authHeader,
          },
          body: JSON.stringify(shouldApprove),
        }
      );

      if (!res.ok) {
        const text = await res.text().catch(() => '');
        throw new Error(text || `Status ${res.status}`);
      }

      // Refresh after successful toggle
      await fetchRegistrations();
    } catch (e) {
      alert(`Failed to update approval: ${e.message}`);
    }
  };

  useEffect(() => {
    if (isAuthenticated) {
      fetchRegistrations();
    } else {
      setError('Not authenticated.');
      setLoading(false);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isAuthenticated]);

  return (
    <>
      <Header />
      <div className="p-6 max-w-6xl mx-auto">
        <h1 className="text-2xl font-bold mb-4">Admin Dashboard</h1>

        {error && <div className="text-red-600 mb-3">{error}</div>}

        {loading ? (
          <div>Loading registrations...</div>
        ) : (
          <table className="w-full border-collapse">
            <thead>
              <tr className="bg-blue-100 text-left">
                <th className="p-2 border">Registration ID</th>
                <th className="p-2 border">User</th>
                <th className="p-2 border">Park</th>
                <th className="p-2 border">Date</th>
                <th className="p-2 border">Time Slot</th>
                <th className="p-2 border">Approved</th>
                <th className="p-2 border">Actions</th>
              </tr>
            </thead>
            <tbody>
              {registrations.length === 0 ? (
                <tr>
                  <td colSpan={7} className="p-4 text-center">
                    No registrations found.
                  </td>
                </tr>
              ) : (
                registrations.map((r) => (
                  <tr key={r.registration_id} className="hover:bg-gray-50">
                    <td className="p-2 border">{r.registration_id}</td>
                    <td className="p-2 border">
                      {r.username ||
                        r.user?.username ||
                        `${r.user?.first_name || ''} ${r.user?.last_name || ''}`.trim() ||
                        'Unknown'}
                    </td>
                    <td className="p-2 border">{r.requested_park}</td>
                    <td className="p-2 border">
                      {r.registration_date
                        ? new Date(r.registration_date).toLocaleDateString()
                        : ''}
                    </td>
                    <td className="p-2 border">
                      {r.start_time} - {r.end_time}
                    </td>
                    <td className="p-2 border">
                      {r.is_approved === 1 ? 'Yes' : 'No'}
                    </td>
                    <td className="p-2 border">
                      <button
                        onClick={() => toggleApproval(r)}
                        className="mr-2"
                      >
                        {r.is_approved === 1 ? 'Revoke' : 'Approve'}
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
};

export default AdminDashboard;
