import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import apiBaseUrl from './config.js';
const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    console.log();
    const newPassword = e.target.value;
    setPassword(newPassword);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    window.localStorage.setItem("isLoggedIn", true);
    console.log('Email:', email);
    console.log('Password:', password);
  };

  return (
    <div className="form">
      <h1>{`${apiBaseUrl}/api/Connection`}</h1>
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
        <Link to="/account"><button type="submit">Zaloguj się</button></Link>
      </form>
    </div>
  );
};

export default LoginPage;
