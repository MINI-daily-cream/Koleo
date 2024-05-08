import { useEffect, useState } from 'react'
import TicketList from './TicketList';
import tickets from './tickets';
import apiBaseUrl from './config';
import { Link } from 'react-router-dom';
import axios from 'axios';

const AccountPanel = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  const [tickets, setTickets] = useState([])

  // const [userId, setuserId] = useState('37040a69-df5e-4479-aa66-28f93843b7ad')
  // const [jwtToken, setJwtToken] = useState('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMzcwNDBhNjktZGY1ZS00NDc5LWFhNjYtMjhmOTM4NDNiN2FkIiwibmJmIjoxNzE1MTQ4MTI4LCJleHAiOjE3MTU3NTI5MjgsImlhdCI6MTcxNTE0ODEyOH0.wfbZ6EtRnaX1aoH5EERGJi0qI7ynACwt8elUk5McXtg');

  function getTickets(){
    const fetchData = async () => {
      try {
       
        
        const response = await axios.get(`${apiBaseUrl}/api/Ticket/list-by-user/${userId}`, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${jwtToken}`
          }
      });
        // localStorage.setItem('jwtToken', response.data.token);
        // localStorage.setItem('id', response.data.id);
        console.log(response)
        setTickets(response.data);
      }
      catch(error) {
        if (error === 'Bad request') {
            console.log('user exists');
            // setPasswordError("Podany użytkownik już istnieje");
        } else {
            console.error('An error occurred:', error);
            // setPasswordError("Wystąpił błąd podczas rejestracji");
        }
      }
    }
    fetchData();
  }

  useEffect( () => {
    getTickets();
  }, [])

  return (
    <div className='account-panel'>
        {/* <h1>Moje Konto   id is {localStorage.getItem('id')}</h1> */}
        <h1>Moje Konto</h1>
        <div className='account-panel-inside'>
            <div className="sidenav">
                <a href="#">Dane użytkownika</a>
                {/* <hr class="solid"></hr> */}
                <a href="#">Bilety</a>
                <a href="#">Statystki</a>
                <a href="#">Osiągnięcia</a>
                <Link to="/complaints">Skargi</Link>
            </div>
            <div className='content'><TicketList tickets={tickets} /></div>
    
        </div>
    </div>
  );
};

export default AccountPanel;
