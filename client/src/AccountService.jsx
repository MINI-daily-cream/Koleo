import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import DeleteAccountButton from "./DeleteButton"; // Import the DeleteAccountButton component
import axios from "axios";
import apiBaseUrl from "./config";

const AccountService = () => {
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

  return (
    <>
      {userInfo ? (
        <div className="user-info">
          <p>
            <strong>Imię:</strong> {userInfo.name}
          </p>
          <p>
            <strong>Nazwisko:</strong> {userInfo.surname}
          </p>
          <p>
            <strong>Adres email:</strong> {userInfo.email}
          </p>
          <Link to="/ChangeEmail">Zmień email</Link> <br />
          <Link to="/ChangePassword">Zmień hasło</Link> <br />
          <DeleteAccountButton />
        </div>
      ) : (
        <p>Ładowanie danych użytkownika...</p>
      )}
    </>
  );
};

export default AccountService;
