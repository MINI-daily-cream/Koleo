import { useEffect, useState } from 'react'
import TicketList from './TicketList';
import tickets from './tickets';
import apiBaseUrl from './config';

const AccountPanel = () => {
  const [userId, setuserId] = useState("C4630E12-DEE8-411E-AF44-E3CA970455CE")
  const [tickets, setTickets] = useState([{}])

  function getTickets(){
    const xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                // const response = JSON.parse(xhr.responseText);
                const response = xhr.responseText;
                console.log(response);
                setTickets(response);
            } else {
                console.error('Błąd pobierania danych:', xhr.status);
                // Obsługa błędów
            }
        }
    };

    // xhr.open('GET', `https://localhost:5001/api/Ticket/list-by-user/${userId}`);
    xhr.open('GET', `${apiBaseUrl}/api/Ticket/list-by-user/${userId}`);
    xhr.withCredentials = true;
    xhr.send();
  }

  useEffect( () => {
    getTickets();
  }, [])

  return (
    <div className='account-panel'>
        <h1>Moje Konto</h1>
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
