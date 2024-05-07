// src/components/TicketList.jsx
import React from 'react';
import Ticket from './Ticket';

const TicketList = ({ tickets }) => {
  return (
    <div className="ticket-list">
      {tickets.map((ticket) => (
        <Ticket 
            key={ticket.key} 
            date={ticket.date} 
            timeDep={ticket.timeDep}
            timeArr={ticket.timeArr}
            passengerName={ticket.passengerName} 
            trainNumber={ticket.trainNumber}
            finalStation={ticket.finalStation}
            departureStation={ticket.departureStation} 
            arrivalStation={ticket.arrivalStation} 
            wagonNumber={ticket.wagonNumber} 
            seatNumber={ticket.seatNumber} 
      />
      ))}
    </div>
  );
};

export default TicketList;
