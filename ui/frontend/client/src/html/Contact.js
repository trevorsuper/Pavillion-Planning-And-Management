import React, { useEffect, useState } from 'react';
import '../css/contact.css';

function Contact() {

  return <div>
    <h1 class="title">Contact Us</h1>
    <div class="form-container">
      <form>
        <div class="form-row">
          <input type="email" placeholder="Email" required></input>
          <input type="tel" placeholder="(111)-222-3333" required></input>
        </div>
        
        <div class="form-row">
          <input type="text" placeholder="First Name" required></input>
          <input type="text" placeholder="Last Name" required></input>
        </div>

        <textarea placeholder="Message..." class="form-message" required></textarea>
        <input type="Submit" class="form-submit"></input>
      </form>
    </div>
  </div>;
}

export default Contact;
