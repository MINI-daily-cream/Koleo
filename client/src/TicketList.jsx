// src/components/TicketList.jsx
import React from 'react';
import Ticket from './Ticket';

const TicketList = ({ tickets }) => {
  return (
    <div className="ticket-list">
      {tickets.map((ticket) => (
        <Ticket 
            key={ticket.id} 
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
