import { useEffect, useState } from 'react'
import TicketList from './TicketList';
import tickets from './tickets';
import apiBaseUrl from './config';
import ComplaintList from './ComplaintList';

const ComplaintPage = (complaints) => {
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


  return (
    <div className='account-panel'>
        <h1>Moje skragi</h1>
        <h1>token is{localStorage.getItem('jwtToken')}</h1>
        <h1>id is{localStorage.getItem('id')}</h1>
        <div className='account-panel-inside'>
            <ComplaintList complaints={complaints}></ComplaintList>
        </div>
    </div>
  );
};

export default ComplaintPage;
