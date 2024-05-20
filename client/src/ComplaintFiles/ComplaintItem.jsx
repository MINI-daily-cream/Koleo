import React from 'react';
import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ComplaintItem = ({ TicketId, Content }) => {
    const [content, setContent] = useState(Content);
    const [ticket, setTicket] = useState(TicketId);
    const [isEditing, setIsEditing] = useState(false);

    const handleEditContent = () => {
        setIsEditing(true);
    };

    const handleSaveContent = async () => {
        // update database
        // try {
        //     const response = await axios.post(`${apiBaseUrl}/api/Complaint/edit/${userId}`, {
        //         "ticketId": ticketId,
        //         "content": content
        //     });
        //     //navigate("/account");
        //     // console.log(response)
        // }
        // catch (error) {
        //     if (error === 'Unauthorized') {
        //         console.log('Unauthorized. Please log in.');
        //         setShowErrorMessage(true);
        //     } else {
        //         console.error('An error occurred:', error);
        //     }
        // };
        setIsEditing(false);
        console.log('Content saved:', content);
    };

    const handleChange = (e) => {
        setContent(e.target.value);
    };
    const handleDelete = async () => {
        // delete from database
        // try {
        //     const response = await axios.delete(`${apiBaseUrl}/api/Complaint/remove/${userId}`, {
        //         "ticketId": ticketId,
        //         "content": content
        //     });
        //     //navigate("/account");
        //     // console.log(response)
        // }
        // catch (error) {
        //     if (error === 'Unauthorized') {
        //         console.log('Unauthorized. Please log in.');
        //         setShowErrorMessage(true);
        //     } else {
        //         console.error('An error occurred:', error);
        //     }
        // };
        setIsEditing(false);
        console.log('Content saved:', content);
    };
    return (
        <div className='connection'>
            <div>
                <div className='header'>Bilet: {ticket}</div>
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
