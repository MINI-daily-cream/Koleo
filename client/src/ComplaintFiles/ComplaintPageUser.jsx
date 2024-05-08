import { useEffect, useState } from 'react'
import ComplaintList from './ComplaintList';
import axios from 'axios';
import apiBaseUrl from '../config';

const ComplaintPage = ({complaints}) => {

    ////
    const [userId, setUserId] = useState("1");
    ////
  const [ticketId, setTicketId] = useState('');
  const [content, setContent] = useState('');
//   const [userId, setuserId] = useState(localStorage.getItem('jwtToken'))
//   const [complaints, setComplaints] = useState([])

//     const fetchData = async () => {
//         try {
//             const response = await axios.get(`${apiBaseUrl}/api/Complaint/list-by-user/${userId}`,{
//             "userId": userId
//             });
//             //localStorage.setItem('jwtToken', response.data.token);
//             //localStorage.setItem('id', response.data.id);
//             setComplaints(response.data);
//             //setShowErrorMessage(false);
//             //navigate("/account");
//             // console.log(response)
//         }
//         catch(error) {
//                 console.error('An error occurred:', error);
//         }
        
//     };

//   useEffect( () => {
//     fetchData();
//   }, [])
const handleCreate = async () => {
    // delete from database
//     try {
//         const response = await axios.post(`${apiBaseUrl}/api/Complaint/make/${userId}`,{
//             "ticketId": ticketId,
//             "content": content
//           });
//           //navigate("/account");
//           // console.log(response)
//         }
//         catch(error) {
//           if (error === 'Unauthorized') {
//               console.log('Unauthorized. Please log in.');
//               setShowErrorMessage(true);
//           } else {
//               console.error('An error occurred:', error);
//           }
//   };
}
  return (
    <div className='account-panel'>
        <h1>Moje skragi</h1>
        <h1>token is{localStorage.getItem('jwtToken')}</h1>
        <h1>id is{localStorage.getItem('id')}</h1>
        <div className='account-panel-inside'>
        <div>
          <label htmlFor="ticketId">Ticket ID:</label>
          <input type="text" id="ticketId" value={ticketId} onChange={(e) => setTicketId(e.target.value)} />
        </div>
        <div>
          <label htmlFor="content">Content:</label>
          <textarea id="content" value={content} onChange={(e) => setContent(e.target.value)} />
        </div>
        <button type="button" className='ConfirmationButton' onClick={handleCreate}>Dodaj skargę</button>
            <ComplaintList complaints={complaints}></ComplaintList>
        </div>
    </div>
  );
};

export default ComplaintPage;
