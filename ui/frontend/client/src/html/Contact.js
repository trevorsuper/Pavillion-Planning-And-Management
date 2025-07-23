import React, { useEffect, useState } from 'react';
import '../css/contact.css';
import { Link } from 'react-router-dom';
import Header from '../components/Header';
import {sendEmail} from '../js/Email';

function Contact() {
  const [formData, setFormData] = useState({
    email: '',
    phone: '',
    firstName: '',
    lastName: '',
    message: ''
  });
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      await sendEmail(formData);
      alert('Email sent successfully!');
      setFormData({
        email: '',
        phone: '',
        firstName: '',
        lastName: '',
        message: ''
      });
    } catch (error) {
      alert('Failed to send email. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div>
      <Header />
      <h1 className="title">Contact Us</h1>
      <div className="form-container">
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} required />
            <input type="tel" name="phone" placeholder="(111)-222-3333" value={formData.phone} onChange={handleChange} required />
          </div>
          
          <div className="form-row">
            <input type="text" name="firstName" placeholder="First Name" value={formData.firstName} onChange={handleChange} required />
            <input type="text" name="lastName" placeholder="Last Name" value={formData.lastName} onChange={handleChange} required />
          </div>

          <textarea placeholder="Message..." name="message" className="form-message" value={formData.message} onChange={handleChange} required />
          <input type="submit" className="form-submit" value={isLoading ? 'Sending...' : 'Send Message'} disabled={isLoading} />
        </form>
      </div>
    </div>
  );
}

export default Contact;