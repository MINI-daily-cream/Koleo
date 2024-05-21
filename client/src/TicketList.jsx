// src/components/TicketList.jsx
import React, { useEffect, useState } from 'react';
import Ticket from './Ticket';
import { useNavigate } from 'react-router-dom';
import HistoryTicket from './TicketHistory/HistoryTicket';
import axios from 'axios';
import apiBaseUrl from './config';

const TicketList = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  const [tickets, setTickets] = useState([])
  const [filter, setFilter] = useState('future'); 
  const navigate = useNavigate();

  const handleBuy = () => {
    navigate('/home');
  };
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
        console.log(response.data)
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
        {tickets.length > 0 ? (
          tickets.map((ticket) =>
            filter === 'past' ? (
              <HistoryTicket
                key={ticket.id}
                ticketId={ticket.id}
                date={ticket.startDate}
                timeDep={ticket.startTime}
                timeArr={ticket.endTime}
                passengerName={`${ticket.name} ${ticket.surname}`}
                trainNumber={ticket.trainNumber}
                providerName={ticket.providerName}
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
                providerName={ticket.providerName}
                passengerName={`${ticket.name} ${ticket.surname}`}
                trainNumber={ticket.trainNumber}
                departureStation={ticket.startStation}
                arrivalStation={ticket.endStation}
                wagonNumber={ticket.wagonNumber}
                seatNumber={ticket.seatNumber}
              />
            )
          )
        ) : (
          <div className='noCount'>
            <div className='textCount'>Obecnie nie ma biletów</div>
            <div className='ButtonAligment'>
              <button type="button" className='ConfirmationButton' onClick={handleBuy}>Kup bilet</button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default TicketList;
