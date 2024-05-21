import React, { useState, useEffect } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import axios from "axios";
import apiBaseUrl from "./config";

const ChangeTicketData = ({}) => {
  const navigate = useNavigate();
  const { state } = useLocation();
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [ticket, setTicket] = useState(
    {ticketId: ''}
);
  const [userInfo, setUserInfo] = useState(null);
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  
  useEffect(() => {
    setTicket(state);
    console.log(state);
}, []);

  const handleNameChange = (e) => {
    setName(e.target.value);
  };
  const handleSurnameChange = (e) => {
    setSurname(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newInfo = {
      //userId: userId,
      //ticketId: state,
      targetName: name,
      targetSurname: surname,
    };
    console.log(userId);
    console.log(state);
    console.log(newInfo);
    try {
      const response = await axios.put(`${apiBaseUrl}/api/Ticket/change-details/${userId}/${state}`, 
        newInfo,
        {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${jwtToken}`
          }
        }
      );
      navigate("/account");
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
      <h1>Zmień dane biletu</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nowe imię:</label>
          <input
            type="text"
            placeholder="Nowe imię"
            value={name}
            onChange={handleNameChange}
            required
          />
        </div>
        <div>
          <label>Nowe nazwisko:</label>
          <input
            type="text"
            placeholder="Nowe nazwisko"
            value={surname}
            onChange={handleSurnameChange}
            required
          />
        </div>
        <button type="submit">Zatwierdź</button>
      </form>

      <Link to="/">Wróć do strony głównej</Link>
    </div>
  );
};

export default ChangeTicketData;
