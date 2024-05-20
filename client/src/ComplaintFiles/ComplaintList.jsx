import React from 'react';
import ComplaintItem from './ComplaintItem';

const ComplaintList = ({ complaints }) => {
  return (
    <div className="connection-list">
      {complaints.map((complaint, index) => (
        <ComplaintItem 
            key={index} 
            TicketId={complaint.ticket_Id} 
            Content={complaint.content}
            ComplaintId={complaint.complaintId}
      />
      ))}
    </div>
  );
};

export default ComplaintList;
