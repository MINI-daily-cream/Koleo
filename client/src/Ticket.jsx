import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const Ticket = ({ date, timeDep, timeArr, passengerName, trainNumber, finalStation, departureStation, arrivalStation, wagonNumber, seatNumber }) => {
  return (
    <div className="ticket">
      <div className="ticket-details">
        <div className="icon">
          <FontAwesomeIcon icon={faCalendar} />
        </div>
        <div className='text'>{date}</div>
      </div>
      <div className="ticket-details">
        <div className="icon">
          <FontAwesomeIcon icon={faClock} />
        </div>
        <div className='text'>{timeDep}</div>
        <div className="icon" id='arrow'>
          {/* <FontAwesomeIcon icon={faGreaterThan} /> */}
          <FontAwesomeIcon icon={faArrowRight} />
        </div>
        <div className='text'>{timeArr}</div>
      </div>
      <div className="ticket-details">
        <div className="icon">
          <FontAwesomeIcon icon={faUser} />
        </div>
        <div>{passengerName}</div>
      </div>
      <div className="ticket-details">
        <div className="icon">
          <FontAwesomeIcon icon={faTrain} />
        </div>
        <div className='text'>{trainNumber}</div>
        <div className="icon" id='arrow'>
          {/* <FontAwesomeIcon icon={faMinus} />
          <FontAwesomeIcon icon={faMinus} />
          <FontAwesomeIcon icon={faMinus} /> */}
          <FontAwesomeIcon icon={faArrowRight} />
        </div>
        <div className='text'>{finalStation}</div>
      </div>
      <div className="ticket-details">
        <div className='text' id='od-do'>Od:</div>
        <div className="icon">
          <FontAwesomeIcon icon={faMapMarkerAlt} />
        </div>
        <div className='text'>{departureStation}</div>
        <div className='od-do-spacer' />
        <div className='text' id='od-do'>Do:</div>
        <div className="icon">
          <FontAwesomeIcon icon={faMapMarkerAlt} />
        </div>
        <div className='text'>{arrivalStation}</div>
      </div>
      <div className="ticket-details">
        <div className="icon">
          <FontAwesomeIcon icon={faTicketAlt} />
        </div>
        <div className='car-seat-number'>Wagon: {wagonNumber}</div>
        <div className='car-seat-number'>Miejsce: {seatNumber}</div>
      </div>
      <button className="return-ticket">Zwróć bilet</button>
    </div>
  );
};

export default Ticket;
