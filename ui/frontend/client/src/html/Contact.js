// Contact.js
import React, { useEffect, useState } from 'react';
import Header from '../components/Header';

function Contact() {
  return (
    <>
      <Header />
      <div>
        <h1>Contact Us</h1>
        <form>
          <input type="email" placeholder="Email" />
          <input type="tel" placeholder="(111)-222-3333" />
          <br />
          <input type="text" placeholder="First Name" />
          <input type="text" placeholder="Last Name" />
          <br />
          <input type="text" placeholder="Message..." />
          <input type="submit" />
        </form>
      </div>
    </>
  );
}

export default Contact;
