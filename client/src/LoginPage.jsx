import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import apiBaseUrl from './config.js';
import axios from 'axios';

const LoginPage = () => {
  axios.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response && error.response.status === 401) {
            return Promise.reject('Unauthorized');
        }
        return Promise.reject(error);
    }
  );

  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showErrorMessage, setShowErrorMessage] = useState(false);

  const handleEmailChange = (e) => {
  setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
  console.log();
  const newPassword = e.target.value;
  setPassword(newPassword);
  };

  function onSubmit(){
    const fetchData = async () => {
      try {
        const response = await axios.post(`${apiBaseUrl}/api/account/login`,{
          "email": email,
          "password": password
        });
        localStorage.setItem('jwtToken', response.data.token);
        localStorage.setItem('id', response.data.id);
        setShowErrorMessage(false);
        navigate("/account");
        // console.log(response)
      }
      catch(error) {
        if (error === 'Unauthorized') {
            console.log('Unauthorized. Please log in.');
            setShowErrorMessage(true);
        } else {
            console.error('An error occurred:', error);
        }
      }
    }
    fetchData();
  }

  return (
    <div className="form">
      {/* <h1>{`${apiBaseUrl}/api/Connection`}</h1> */}
      <h1>Zaloguj się</h1>
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
        {/* <p className='bottom-text' disabled>Nie masz jeszcze konta? <a href="/register">Zarejestruj się</a></p> */}
        <p className='bottom-text' disabled>Nie masz jeszcze konta? <Link to="/register">Zarejestruj się</Link></p>
        {showErrorMessage ? <p className='errorMessage'>Błędny email lub hasło</p> : <></>}
        <button type="button" onClick={onSubmit}>Zaloguj się</button>
      </form>
    </div>
  );
};

export default LoginPage;
