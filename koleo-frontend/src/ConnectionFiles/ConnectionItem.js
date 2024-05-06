import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ConnectionItem = ({ StartDate, EndDate, StartTime, EndTime,
  Duration, TrainNumber, StartStation, EndStation, ProviderName,
  SourceCity, DestinationCity, DepartureTime, ArrivalTime, KmNumber }) => {
  return (
    <div className='connection'>
      <div className='connection-text-column'>
        <div className='text'>Odjazd</div>
        <div className='text'>Przyjazd</div>
      </div>
      <div className='connection-text-column'>
        <div className='time'>{StartTime}</div>
        <div className="icon" id='arrow'>
          <FontAwesomeIcon icon={faArrowRight} />
        </div>
        <div className='time'>{EndTime}</div>
      </div>
      <div className='connection-text-column'>
        <div className='text'>{StartDate}</div>
        <div className='text'>{EndDate}</div>
      </div>
      <div className='connection-text-column'>
        <div className='text'>{StartStation}</div>
        <div className='text'>{EndStation}</div>
      </div>
      <div className='connection-row-info'>
        <div className="icon">
          <FontAwesomeIcon icon={faClock} />
        </div>
        <div className='text'>{Duration}</div>
      </div>
      <div className='connection-row-info'>
        <div className="icon">
          <FontAwesomeIcon icon={faTrain} />
        </div>
        <div className='text'>{ProviderName}</div>
      </div>
      <div className='ButtonAligment'>
      <Link to="/ticketConfirmation"><button type="submit" className='ConfirmationButton'>Wybierz</button></Link>
      </div>
    </div>
  );
};

export default ConnectionItem;
