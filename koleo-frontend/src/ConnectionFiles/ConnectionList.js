// src/components/TicketList.js
import React from 'react';
import ConnectionItem from './ConnectionItem';

const ConnectionList = ({ connections }) => {
  return (
    <div className="connection-list">
      {connections.map((connection, index) => (
        <ConnectionItem 
            key={index} 
            StartDate={connection.startDate} 
            EndDate={connection.endDate}
            StartTime={connection.startTime}
            EndTime={connection.endTime} 
            Duration={connection.duration}
            TrainNumber={connection.trainNumber}
            StartStation={connection.startStation} 
            EndStation={connection.endStation} 
            ProviderName={connection.providerName} 
            SourceCity={connection.sourceCity} 
            DestinationCity={connection.destinationCity} 
            DepartureTime={connection.departureTime} 
            ArrivalTime={connection.arrivalTime} 
            KmNumber={connection.kmNumber} 
      />
      ))}
    </div>
  );
};

export default ConnectionList;
