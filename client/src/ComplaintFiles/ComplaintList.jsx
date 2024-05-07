import React from 'react';
import ComplaintItem from './ComplaintItem';

const ComplaintList = ({ complaints }) => {
  return (
    <div className="connection-list">
      {complaints.map((complaint, index) => (
        <ComplaintItem 
            key={index} 
            TicketId={complaint.ticketId} 
            Content={complaint.content}
      />
      ))}
    </div>
  );
};

export default ComplaintList;
