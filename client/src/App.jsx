import {React, Component} from "react";
import {Routes, BrowserRouter as Router, Route } from 'react-router-dom'
import './App.css';
import LoginPage from './LoginPage.jsx';
import RegistrationPage from './RegistrationPage.jsx';
import TicketList from "./TicketList.jsx";
import tickets from "./tickets.jsx";
import Navbar from "./Navbar.jsx";
import AccountPanel from "./AccountPanel.jsxx";
import HomePage from "./HomePage.jsx";
import TicketConformation from "./TicketConfirmation.jsx";
import { connectionsData } from "./connections.jsxx";
import FoundConnectionList from "./FoundConnections.jsx";

class App extends Component{

constructor(props)
{
  super(props);
}

render() {
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
      </Routes>
    </Router>
  );
}
}
export default App;
