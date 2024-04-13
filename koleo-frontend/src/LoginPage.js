import React, { useState } from 'react';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Tutaj można dodać logikę rejestracji
    console.log('Email:', email);
    console.log('Password:', password);
  };

  return (
    <div className="form">
      <h1>Zaloguj się</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Adres e-mail:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={handleEmailChange}
            required
          />
        </div>
        <div>
          <label htmlFor="password">Hasło:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={handlePasswordChange}
            required
          />
        </div>
        <p className='bottom-text' disabled>Nie masz jeszcze konta? <a href="/register">Zarejestruj się</a></p>
        <button type="submit">Zaloguj się</button>
      </form>
    </div>
  );
};

export default LoginPage;