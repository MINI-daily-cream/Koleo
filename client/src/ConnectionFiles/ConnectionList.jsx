// src/components/TicketList.js
import React from 'react';
import ConnectionItem from './ConnectionItem';

const ConnectionList = ({ connections }) => {
  return (
    <div className="connection-list">
      {connections.map((connection, index) => (
        <ConnectionItem 
          key={index} 
          connection={connection}
      />
      ))}
    </div>
  );
};

export default ConnectionList;
