import React from "react";
import TicketList from "./TicketList";
import tickets from "./tickets";
import { Link } from "react-router-dom";

const AccountPanel = () => {
  return (
    <div className="account-panel">
      <h1>Moje Konto</h1>
      <div className="account-panel-inside">
        <div className="sidenav">
          <Link to="/AccountService">Dane użytkownika</Link>

          {/* <hr class="solid"></hr> */}
          <a href="#">Bilety</a>
          <Link to="/Statistics">Statystyki</Link>
          <a href="#">Osiągnięcia</a>
        </div>
        {/* <div className='content'><TicketList tickets={tickets} /></div> */}
        <TicketList tickets={tickets} />
      </div>
    </div>
  );
};

export default AccountPanel;
