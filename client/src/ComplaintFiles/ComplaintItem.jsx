import React from 'react';
import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import apiBaseUrl from '../config';
import { faTrain, faCalendar, faClock, faGreaterThan, faMinus, faArrowRight, faUser, faMapMarkerAlt, faTicketAlt } from '@fortawesome/free-solid-svg-icons';

const ComplaintItem = ({ TicketId, Content, ComplaintId }) => {
    const [content, setContent] = useState(Content);
    const [ticket, setTicket] = useState(TicketId);
    const [ticketData, setTicketData] = useState(
        {startStation : '', endStation: '', startDate: ''}
    );
    const [complaintId, setComplaintId] = useState(ComplaintId);
    const [isEditing, setIsEditing] = useState(false);
    const [userId, setuserId] = useState(localStorage.getItem('id'))
    const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));

    const fetchData = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/api/Ticket/get-ticket-for-complaint/${userId}/${ticket}`,{
              headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${jwtToken}`
              }
            });
            console.log(response.data);
            setTicketData(response.data);
        }
        catch(error) {
          if (error === 'Bad request') {
              console.error('user exists');
          } else {
              console.error('An error occurred:', error);
          }
        }
        
    };

  useEffect( () => {
    fetchData();
  }, [])

    const handleEditContent = () => {
        
        setIsEditing(true);
    };

    const handleSaveContent = async () => {
        setIsEditing(false);
        console.log('Content saved:', content);
        // update database
        try {
            const requestBody = {
                ticketId: ticket,
                content: content,
                complaintId: complaintId
                };
            const response = await axios.put(`${apiBaseUrl}/api/Complaint/edit/${complaintId}`, requestBody, {
              headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${jwtToken}`
              }
            });
            console.log(response.data);
            if(response.status == 200) 
            {
                alert("Zmiany zostały zapisane");
                window.location.reload();
            }
            else
            {
                alert("Wystąpił bląd");
            }
        }
        catch (error) {
            if (error === 'Unauthorized') {
                console.log('Unauthorized. Please log in.');
                setShowErrorMessage(true);
            } else {
                console.error('An error occurred:', error);
            }
        };
    };

    const handleChange = (e) => {
        setContent(e.target.value);
    };
    const handleDelete = async () => {
        // delete from database
        try {
            const response = await axios.delete(`${apiBaseUrl}/api/Complaint/remove/${complaintId}`,{
              headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${jwtToken}`
              }
            });
            console.log(response.data);
            if(response.status == 200) 
            {
                alert("Usunięto skargę");
                window.location.reload();
            }
            else
            {
                alert("Wystąpił bląd");
            }
        }
        catch (error) {
            if (error === 'Unauthorized') {
                console.log('Unauthorized. Please log in.');
                setShowErrorMessage(true);
            } else {
                console.error('An error occurred:', error);
            }
        };
        setIsEditing(false);
        console.log('Content saved:', content);
    };
    return (
        <div className='connection'>
            <div>
                <div className='header'>Bilet: {ticketData.startStation} - {ticketData.endStation}, {ticketData.startDate}</div>
                <div className='ComplaintBox1'>
                    {isEditing ? (
                        <div>
                        <div className='header'>Treść:</div>
                        <textarea
                            className="content"
                            value={content}
                            onChange={handleChange}
                            maxLength={200}
                        />
                        </div>
                    ) : (
                        <div>
                            <div className='header'>Treść:</div>
                            <div className='content'>{content}</div>
                        </div>
                    )}
                    {isEditing ? (
                        <button className='ConfirmationButton' onClick={handleSaveContent}>Zapisz</button>
                    ) : (
                        <button className='ConfirmationButton' onClick={handleEditContent}>Edytuj</button>
                    )}
                    <button type="submit" className='ConfirmationButton' onClick={handleDelete}>Usuń</button>
                </div>
            </div>
        </div>
    );
};

export default ComplaintItem;
