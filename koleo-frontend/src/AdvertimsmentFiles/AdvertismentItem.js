import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const AdvertismentItem = ({ AdContent, AdLinkUrl, AdImageUrl, AdOwner }) => {
  return (
    <div className='ad-block'>
        <div className='adImage'>
            <Link to={AdLinkUrl}>
                <img src={AdImageUrl} alt="Advertisement" />
            </Link>
        </div>
        <div className='ad-content'>
            <Link to={AdLinkUrl}>
                <p>{AdContent}</p>
            </Link>
        </div>
    </div>
  );
};

export default AdvertismentItem;
