import React from 'react';
import '../css/login.css';

function Login() {
  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center">
      {/* Login Box */}
      <div className="bg-white rounded-lg shadow-lg p-10 w-full max-w-md">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-xl text-gray-700 mt-2">Login</h1>
        </div>

        {/* Form */}
        <form>
          <div className="mb-4">
            <label htmlFor="username" className="block text-gray-700 mb-1">
              Username
            </label>
            <input
              id="username"
              type="text"
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Enter your username"
            />
          </div>

          <div className="mb-2">
            <label htmlFor="password" className="block text-gray-700 mb-1">
              Password
            </label>
            <input
              id="password"
              type="password"
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Enter your password"
            />
          </div>

          {/* Forgot Links */}
          <div className="flex justify-between text-sm text-blue-700 mt-1 mb-6">
            <button
              type="button"
              className="hover:underline"
              onClick={() => alert('Redirect to Forgot Username')}
            >
              Forgot Username?
            </button>
            <button
              type="button"
              className="hover:underline"
              onClick={() => alert('Redirect to Forgot Password')}
            >
              Forgot Password?
            </button>
          </div>

          {/* Buttons */}
          <div className="flex flex-col space-y-3">
            <button
              type="submit"
              className="w-full bg-blue-900 text-white py-2 rounded hover:bg-blue-800 transition"
            >
              Login
            </button>
            <button
              type="button"
              className="w-full border border-blue-900 text-blue-900 py-2 rounded hover:bg-blue-50 transition"
              onClick={() => alert('Redirect to Sign Up')}
            >
              Sign Up
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default Login;