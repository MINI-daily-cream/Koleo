import React from 'react';
import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ComplaintItem = ({ TicketId, Content }) => {
    const [content, setContent] = useState(Content);
    const [isEditing, setIsEditing] = useState(false);
  
    const handleEditContent = () => {
      setIsEditing(true);
    };
  
    const handleSaveContent = () => {
        // update database
      setIsEditing(false);
      console.log('Content saved:', content);
    };
  
    const handleChange = (e) => {
      setContent(e.target.value);
    };
    const handleDelete = () => {
        // delete from database
      };
  return (
    <div className='connection'>
        <div>
      <div className='header'>Bilet: {TicketId}</div>
      {isEditing ? (
        <textarea
          className="content"
          value={content}
          onChange={handleChange}
          maxLength={200}
        />
      ) : (
        <div className='content'>{content}</div>
      )}
      {isEditing ? (
        <button className='ConfirmationButton' onClick={handleSaveContent}>Zapisz</button>
      ) : (
        <button className='ConfirmationButton' onClick={handleEditContent}>Edytuj</button>
      )}
      <button type="submit" className='ConfirmationButton' onClick={handleDelete}>Usuń</button>
      </div>
    </div>
  );
};

export default ComplaintItem;
