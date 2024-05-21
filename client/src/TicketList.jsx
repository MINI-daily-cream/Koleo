// src/components/TicketList.jsx
import React, { useEffect, useState } from 'react';
import Ticket from './Ticket';
import HistoryTicket from './TicketHistory/HistoryTicket';
import axios from 'axios';
import apiBaseUrl from './config';

const TicketList = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  const [tickets, setTickets] = useState([])
  const [filter, setFilter] = useState('future'); 

  function getTickets(){
    let endpoint = '';
    switch (filter) {
      case 'past':
        endpoint = `${apiBaseUrl}/api/Ticket/list-by-user-past-connections/${userId}`;
        break;
      case 'future':
        endpoint = `${apiBaseUrl}/api/Ticket/list-by-user-future-connections/${userId}`;
        break;
      default:
        endpoint = `${apiBaseUrl}/api/Ticket/list-by-user/${userId}`;
    }
    const fetchData = async () => {
      try {
        const response = await axios.get(endpoint, {
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

  useEffect(() => {
    getTickets(filter);
  }, [filter]);

  return (
    <div className='ticketBox'>
      <div className="filter-buttons">
        <button className="filter-button" onClick={() => setFilter('future')}>Pokaż nadchodzące przejazdy</button>
        <button className="filter-button" onClick={() => setFilter('past')}>Pokaż historie przejazdów</button>
      </div>
      <div className="ticket-list">

      {tickets.map((ticket) =>
          filter === 'past' ? (
            <HistoryTicket
              key={ticket.id}
              ticketId={ticket.id}
              date={ticket.startDate}
              timeDep={ticket.startTime}
              timeArr={ticket.endTime}
              passengerName={`${ticket.name} ${ticket.surname}`}
              trainNumber={ticket.trainNumber}
              departureStation={ticket.startStation}
              arrivalStation={ticket.endStation}
              wagonNumber={ticket.wagonNumber}
              seatNumber={ticket.seatNumber}
            />
          ) : (
            <Ticket
              key={ticket.id}
              ticketId={ticket.id}
              date={ticket.startDate}
              timeDep={ticket.startTime}
              timeArr={ticket.endTime}
              passengerName={`${ticket.name} ${ticket.surname}`}
              trainNumber={ticket.trainNumber}
              departureStation={ticket.startStation}
              arrivalStation={ticket.endStation}
              wagonNumber={ticket.wagonNumber}
              seatNumber={ticket.seatNumber}
            />
          )
        )}
      </div>
    </div>
  );
};

export default TicketList;
