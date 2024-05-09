// src/components/TicketList.jsx
import React, { useEffect, useState } from 'react';
import Ticket from './Ticket';
import axios from 'axios';
import apiBaseUrl from './config';

const TicketList = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  const [tickets, setTickets] = useState([])

  function getTickets(){
    const fetchData = async () => {
      try {
        const response = await axios.get(`${apiBaseUrl}/api/Ticket/list-by-user/${userId}`, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${jwtToken}`
          }
        });
        // console.log(response)
        setTickets(response.data);
      }
      catch(error) {
        if (error === 'Bad request') {
            console.error('user exists');
        } else {
            console.error('An error occurred:', error);
        }
      }
    }
    fetchData();
  }

  useEffect( () => {
    getTickets();
  }, [])

  return (
    <div className="ticket-list">
      {tickets.map((ticket) => (
        <Ticket 
            key={ticket.id}
            ticketId={ticket.id}
            date={ticket.startDate} 
            timeDep={ticket.startTime}
            timeArr={ticket.endTime}
            passengerName={`${ticket.name} ${ticket.surname}`} 
            trainNumber={ticket.trainNumber}
            finalStation={ticket.endStation}
            departureStation={ticket.startStation} 
            arrivalStation={ticket.endStation} 
            wagonNumber={ticket.wagonNumber} 
            seatNumber={ticket.seatNumber} 
      />
      ))}
    </div>
  );
};

export default TicketList;
