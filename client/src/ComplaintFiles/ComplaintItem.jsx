import React from 'react';
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
      setIsEditing(false);
      // Here you can implement logic to save the edited content
      // For demonstration purposes, I'm just logging it
      console.log('Content saved:', content);
    };
  
    const handleChange = (e) => {
      setContent(e.target.value);
    };
  return (
    <div className='connection'>
      <div className='header'>{TicketId}</div>
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
      <button type="submit" className='ConfirmationButton'>Usuń</button>
    </div>
  );
};

export default ComplaintItem;
