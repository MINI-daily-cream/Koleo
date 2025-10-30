import { useEffect, useState } from "react";
import TicketList from "./TicketList";
import tickets from "./tickets";
import apiBaseUrl from "./config";
import { Link, Outlet } from "react-router-dom";
import axios from "axios";

const AccountPanel = () => {
  return (
    <div className="account-panel">
      {/* <h1>Moje Konto   id is {localStorage.getItem('id')}</h1> */}
      <h1>Moje Konto</h1>
      <div className="account-panel-inside">
        <div className="sidenav">
          <Link to="info">Dane użytkownika</Link>
          <Link to="tickets">Bilety</Link>
          <Link to="statistics">Statystki</Link>
          <Link to="achievements">Osiągnięcia</Link>
          <Link to="rankings">Rankingi</Link>

          <Link to="/complaints">Skargi</Link>
        </div>
        {/* <div className='content'><TicketList tickets={tickets} /></div> */}
        <div className="content">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default AccountPanel;
