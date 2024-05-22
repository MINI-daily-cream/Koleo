import React from 'react';
import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ConnectionItem = ({ connection }) => {
    const navigate = useNavigate();

    function goToConfirmation() {
      // navigate(`/ticketConfirmation/${connection}`);
      console.log("przed navigate");
      console.log(connection);
      navigate(`/ticketConfirmation`, {state: connection });
      // navigate(`/ticketConfirmation`, {state: {connection} });
    }

    // useEffect( () => {
    //   console.log(connection);
    // }, [])

  return (
    <div className='connection'>
      <div className='connection-text-column'>
        <div className='text'>Odjazd</div>
        <div className='text'>Przyjazd</div>
      </div>
      <div className='connection-text-column'>
        <div className='time'>{connection.departureTime}</div>
        <div className="icon" id='arrow'>
          <FontAwesomeIcon icon={faArrowRight} />
        </div>
        <div className='time'>{connection.arrivalTime}</div>
      </div>
      <div className='connection-text-column'>
        <div className='text'>{connection.startDate}</div>
        <div className='text'>{connection.endDate}</div>
      </div>
      <div className='connection-text-column'>
        <div className='text'>{connection.startStation}</div>
        <div className='text'>{connection.endStation}</div>
      </div>
      <div className='connection-row-info'>
        <div className="icon">
          <FontAwesomeIcon icon={faClock} />
        </div>
        <div className='text'>{connection.duration}</div>
      </div>
      <div className='connection-row-info'>
        <div className="icon">
          <FontAwesomeIcon icon={faTrain} />
        </div>
        <div className='text'>{connection.providerName}</div>
      </div>
      <div className='ButtonAligment'>
      {/* <Link to="/ticketConfirmation"> */}
      <button type="button" className='ConfirmationButton' onClick={goToConfirmation}>Wybierz</button>
        {/* </Link> */}
      </div>
    </div>
  );
};

export default ConnectionItem;
