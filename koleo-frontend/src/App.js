import {React, Component} from "react";
import {Routes, BrowserRouter as Router, Route } from 'react-router-dom'
import './App.css';
import LoginPage from './LoginPage';
import RegistrationPage from './RegistrationPage';
import React from "react";
import "./App.css";
import LoginPage from "./LoginPage";
import RegistrationPage from "./RegistrationPage";
import TicketList from "./TicketList";
import tickets from "./tickets.js";
import Navbar from "./Navbar.js";
import AccountPanel from "./AccountPanel.js";
import HomePage from "./HomePage.js";
import TicketConformation from "./TicketConfirmation";
import { connectionsData } from "./connections.js";
import FoundConnectionList from "./FoundConnections";

class App extends Component{

constructor(props)
{
  super(props);
}

render() {
import AccountService from "./AccountService.js";
import ChangeEmail from "./ChangeEmail.js";
import StatisticsService from "./StatisticsService.js";
import ChangePassword from "./ChangePassword";
function App() {
  const login = window.localStorage.getItem("isLoggedIn");
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/home" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegistrationPage />} />
        <Route path="/account" element={ <AccountPanel />} />
        {/* <Route path="/account" element={login ? <AccountPanel /> : <LoginPage />} /> */}
              <Route path="/tickets" element={<AccountPanel />} />
              <Route path="/tickets" element={<TicketList tickets={tickets} />} />
              <Route path="/ticketConfirmation" element={<TicketConformation />} />
              <Route path="/FoundConnections" element={<FoundConnectionList />} />
        <Route
          path="/account"
          element={login ? <AccountPanel /> : <LoginPage />}
        />
        <Route path="/AccountService" element={<AccountService />} />
        <Route path="/Statistics" element={<StatisticsService />} />

        <Route path="/ChangeEmail" element={<ChangeEmail />} />
        <Route path="/ChangePassword" element={<ChangePassword />} />

        {/* <Route path="/tickets" element={<AccountPanel />} /> */}
        {/* <Route path="/tickets" element={<TicketList tickets={tickets} />} /> */}
      </Routes>
    </Router>
  );
}
}
export default App;
