// src/components/AdminRoute.js
import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const asBool = (v) => {
  if (v === true) return true;
  if (v === 'true') return true;
  if (v === 1 || v === '1') return true;
  return false;
};

const AdminRoute = ({ children }) => {
  const { user, isAuthenticated } = useAuth();

  if (!isAuthenticated) return <Navigate to="/login" replace />;
  if (!asBool(user?.is_admin) && !asBool(user?.isAdmin)) return <Navigate to="/home" replace />;

  return <>{children}</>;
};

export default AdminRoute;

