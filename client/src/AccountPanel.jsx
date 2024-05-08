import { useEffect, useState } from 'react'
import TicketList from './TicketList';
import tickets from './tickets';
import apiBaseUrl from './config';
import axios from 'axios';

const AccountPanel = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [tickets, setTickets] = useState([{}])

  function getTickets(){
    const fetchData = async () => {
      try {
        const response = await axios.get(`${apiBaseUrl}/api/Ticket/list-by-user/${userId}`);
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
        <h1>Moje Konto   id is {localStorage.getItem('id')}</h1>
        <div className='account-panel-inside'>
            <div className="sidenav">
                <a href="#">Dane użytkownika</a>
                {/* <hr class="solid"></hr> */}
                <a href="#">Bilety</a>
                <a href="#">Statystki</a>
                <a href="#">Osiągnięcia</a>
            </div>
            {/* <div className='content'><TicketList tickets={tickets} /></div> */}
            <TicketList tickets={tickets} />
        </div>
    </div>
  );
};

export default AccountPanel;
