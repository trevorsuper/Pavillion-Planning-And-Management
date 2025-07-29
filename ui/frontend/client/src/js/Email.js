export const sendEmail = async (formData) => {
  try {
    const response = await fetch('/api/send-email', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        to: 'email@gmail.com',  //replace with actual email address you want emails sent to
        from: formData.email,
        subject: `New Contact Form Message from ${formData.firstName} ${formData.lastName}`,
        message: formData.message,
        phone: formData.phone
      })
    });
    
    if (!response.ok) {
      throw new Error('Failed to send email');
    }
    
    return await response.json();
  } catch (error) {
    console.error('Error sending email:', error);
    throw error;
  }
};