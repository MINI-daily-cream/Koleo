import { useEffect, useState } from 'react'
import { Link} from "react-router-dom";
import ComplaintList from './ComplaintList';
import axios from 'axios';
import apiBaseUrl from '../config';
import SelectTickets from '../sharedComponents/SelectListTicketForComplaints';

const ComplaintPage = ({}) => {

    ////
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  const [tickets, setTickets] = useState([])
    ////
  const [ticket, setTicket] = useState('');
  const [content, setContent] = useState('');
//   const [userId, setuserId] = useState(localStorage.getItem('jwtToken'))
  const [complaintList, setComplaints] = useState([])

    const fetchData = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/api/Complaint/list-by-user-unanswered/${userId}`,{
              headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${jwtToken}`
              }
            });
            console.log(response.data);
            setComplaints(response.data);
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

const handleCreate = async () => {
    try {
      //console.log(ticket.id);
      console.log(ticket);

      const requestBody = {
        ticketId: ticket,
        content: content,
        complaintId: ''
        };
        const response = await axios.put(`${apiBaseUrl}/api/Complaint/make/${userId}`, requestBody, {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${jwtToken}`
          }
          });
          alert("Skarga została stworzona");
          setContent('');
          window.location.reload();
        }
        catch(error) {
          if (error === 'Unauthorized') {
              console.log('Unauthorized. Please log in.');
              setShowErrorMessage(true);
          } else {
              console.error('An error occurred:', error);
          }
  };
}

  return (
    <div className='account-panel'>
        <h1>Moje skragi</h1>
        {/* <h1>token is{localStorage.getItem('jwtToken')}</h1> */}
        {/* <h1>id is{localStorage.getItem('id')}</h1> */}
        <div className='account-panel-inside'>
        <div>
          <div className='ComplaintBox'>
            {/* <label htmlFor="ticketId">Ticket:</label>
            <input type="text" id="ticket" value={ticket} onChange={(e) => setTicket(e.target.value)} /> */}
            <SelectTickets onTicketChange={setTicket} LabelName={"Wybierz bilet"}></SelectTickets>
          </div>
          <div className='customTextArea'>
            {/* <label htmlFor="content">Content:</label> */}
            <div className='header'>Treść:</div>
            <textarea id="content" value={content} maxLength={200} placeholder="Tekst skargi" onChange={(e) => setContent(e.target.value)} />
          </div>
          <div className='ButtonAligment'>
          <button type="button" className='ConfirmationButton' onClick={handleCreate}>Dodaj skargę</button>
          </div>
        </div>
            <ComplaintList complaints={complaintList}></ComplaintList>
        </div>
        <Link to="/account"><button type="button" className="ConfirmationButton">Wróć na stronę główną</button></Link>
    </div>
  );
};

export default ComplaintPage;
