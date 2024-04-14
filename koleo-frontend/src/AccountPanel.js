import React from 'react';
import TicketList from './TicketList';
import tickets from './tickets';

const AccountPanel = () => {
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
