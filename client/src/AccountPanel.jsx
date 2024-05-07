import { useEffect, useState } from 'react'
import TicketList from './TicketList';
import tickets from './tickets';
import apiBaseUrl from './config';
import axios from 'axios';

const AccountPanel = () => {
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [tickets, setTickets] = useState([{}])

  // function getTickets(){
  //   const xhr = new XMLHttpRequest();
  //   xhr.onreadystatechange = function () {
  //       if (xhr.readyState === XMLHttpRequest.DONE) {
  //           if (xhr.status === 200) {
  //               // const response = JSON.parse(xhr.responseText);
  //               const response = xhr.responseText;
  //               console.log(response);
  //               setTickets(response);
  //           } else {
  //               console.error('Błąd pobierania danych:', xhr.status);
  //               // Obsługa błędów
  //           }
  //       }
  //   };

  //   // xhr.open('GET', `https://localhost:5001/api/Ticket/list-by-user/${userId}`);
  //   xhr.open('GET', `${apiBaseUrl}/api/Ticket/list-by-user/${userId}`);
  //   xhr.withCredentials = true;
  //   xhr.send();
  // }

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
        <h1>Moje Konto</h1>
        <h1>token is{localStorage.getItem('jwtToken')}</h1>
        <h1>id is{localStorage.getItem('id')}</h1>
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
