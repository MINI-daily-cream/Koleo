import axios from 'axios';
import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import apiBaseUrl from './config';

const RegistrationPage = () => {
  axios.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response && error.response.status === 400) {
            return Promise.reject('Bad request');
        }
        return Promise.reject(error);
    }
  );

  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    if (confirmPassword && newPassword !== confirmPassword) {
      setPasswordError("Hasła nie pasują do siebie");
    } else {
      setPasswordError("");
    }
  };

  const handleConfirmPasswordChange = (e) => {
    const newPassword = e.target.value;
    setConfirmPassword(newPassword);
    if (newPassword !== password) {
      setPasswordError("Hasła nie pasują do siebie");
    } else {
      setPasswordError("");
    }
  };

  function onSubmit(){
    if (password !== confirmPassword) {
      setPasswordError("Hasła nie pasują do siebie");
      return;
    }

    const fetchData = async () => {
      try {
        const response = await axios.post(`${apiBaseUrl}/api/account/register`,{
          "email": email,
          "password": password
        });
        localStorage.setItem('jwtToken', response.data.token);
        localStorage.setItem('id', response.data.id);
        navigate("/account");
        // console.log(response)
      }
      catch(error) {
        if (error === 'Bad request') {
            console.log('user exists');
            setPasswordError("Podany użytkownik już istnieje");
        } else {
            console.error('An error occurred:', error);
            setPasswordError("Wystąpił błąd podczas rejestracji");
        }
      }
    }
    fetchData();
  }

  return (
    <div className="form">
      <h1>Zarejestruj się</h1>
      <form>
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
        <div>
          <label htmlFor="confirmPassword">Powtórz hasło:</label>
          <input
            type="password"
            id="confirmPassword"
            value={confirmPassword}
            onChange={handleConfirmPasswordChange}
            required
          />
          {passwordError && <p className='errorMessage'>{passwordError}</p>}
        </div>
        <p className='bottom-text' disabled>Masz już konto? <Link to="/login">Zaloguj się</Link></p>
          <button type="button" onClick={onSubmit}>Zarejestruj się</button>
      </form>
    </div>
  );
};

export default RegistrationPage;