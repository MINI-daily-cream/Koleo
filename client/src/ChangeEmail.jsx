import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import apiBaseUrl from "./config";

const ChangeEmail = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [userInfo, setUserInfo] = useState(null);
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));

  function getUserData() {
    const fetchData = async () => {
      try {
        const response = await axios.get(`${apiBaseUrl}/api/Account/${userId}`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            }
        });
        setUserInfo(response.data);
      } 
      catch(error) {
        if (error === 'Bad request') {
            console.error('user exists');
        } else {
            console.error('An error occurred:', error);
        }
      }
    };
    fetchData();
  }

  useEffect( () => {
    getUserData();
  }, [])

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newInfo = {
      name: userInfo.name,
      surname: userInfo.surname,
      email: email,
    };

    try {
      const response = await axios.put(`${apiBaseUrl}/api/Account/${userId}`, 
        newInfo,
        {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${jwtToken}`
          }
        }
      );
      navigate("/account/info");
    }
    catch(error) {
      if (error === 'Bad request') {
          console.error('user exists');
      } else {
          console.error('An error occurred:', error);
      }
    }
  };

  return (
    <div className="form">
      <h1>Zmień email</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nowy adres email:</label>
          <input
            type="email"
            placeholder="Nowy adres email"
            value={email}
            onChange={handleEmailChange}
            required
          />
        </div>

        <button type="submit">Zatwierdź</button>
      </form>

      <Link to="/">Wróć do strony głównej</Link>
    </div>
  );
};

export default ChangeEmail;
