import React from 'react';
import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ConnectionItem = ({ connection }) => {
    const navigate = useNavigate();

    function goToConfirmation() {
      console.log(connection);
      navigate(`/ticketConfirmation`, {state: connection });
    }

    // useEffect( () => {
    //   console.log(connection);
    // }, [])

  return (
    <div className='connection'>
      {connection.map((singleConnection, index) => (
        <div key={index}>
          <div className='connection-text-column'>
            <div className='text'>Odjazd</div>
            <div className='text'>Przyjazd</div>
          </div>
          <div className='connection-text-column'>
            <div className='time'>{singleConnection.departureTime}</div>
            <div className="icon" id='arrow'>
              <FontAwesomeIcon icon={faArrowRight} />
            </div>
            <div className='time'>{singleConnection.arrivalTime}</div>
          </div>
          <div className='connection-text-column'>
            <div className='text'>{singleConnection.startDate}</div>
            <div className='text'>{singleConnection.endDate}</div>
          </div>
          <div className='connection-text-column'>
            <div className='text'>{singleConnection.startStation}</div>
            <div className='text'>{singleConnection.endStation}</div>
          </div>
          <div className='connection-row-info'>
            <div className="icon">
              <FontAwesomeIcon icon={faClock} />
            </div>
            <div className='text'>{singleConnection.duration}</div>
          </div>
          <div className='connection-row-info'>
            <div className="icon">
              <FontAwesomeIcon icon={faTrain} />
            </div>
            <div className='text'>{singleConnection.providerName}</div>
          </div>
        </div>
      ))}
        <div className='ButtonAligment'>
          <button type="button" className='ConfirmationButton' onClick={goToConfirmation}>Wybierz</button>
        </div>
    </div>
  );
};

export default ConnectionItem;
