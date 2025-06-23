// FAQ.js
import React, { useState } from 'react';
import '../css/FAQ.css';
import { Link } from 'react-router-dom';
import Header from '../components/Header';

const faqs = [
  {
    question: 'How do I reserve a pavilion?',
    answer: (
      <>
        You can reserve a pavilion by visiting our <Link to="/book">reservation page</Link>.
      </>
    ),
  },
  {
    question: 'Can I register for multiple events?',
    answer: 'Yes, you can register for as many events as you like, unless noted otherwise.',
  },
  {
    question: 'Where can I view upcoming park events?',
    answer: (
      <>
        You can view our park events list on the <Link to="/events">Community Park Events page</Link>.
      </>
    ),
  },
  {
    question: 'Is there a waitlist if an event is full?',
    answer: 'Yes, if an event is full, you will automatically be added to the waitlist.',
  },
];

const FAQ = () => {
  const [openIndex, setOpenIndex] = useState(null);

  const toggleFAQ = (index) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  return (
    <>
      <Header />
      <div className="faq-container">
        <h1 className="faq-title">Frequently Asked Questions</h1>
        {faqs.map((item, index) => (
          <div key={index} className="faq-item">
            <button className="faq-question" onClick={() => toggleFAQ(index)}>
              {item.question}
            </button>
            {openIndex === index && <div className="faq-answer">{item.answer}</div>}
          </div>
        ))}
      </div>
    </>
  );
};

export default FAQ;
