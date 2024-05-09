import React from 'react';
import { Routes, BrowserRouter as Router, Route } from 'react-router-dom';
import { createRoot } from 'react-dom/client'; // Correct import for React 18
import './App.css';
import './index.css';
import LoginPage from './LoginPage.jsx';
import RegistrationPage from './RegistrationPage.jsx';
import TicketList from "./TicketList.jsx";
import tickets from "./tickets.jsx";
import Navbar from "./Navbar.jsx";
import AccountPanel from "./AccountPanel.jsx";
import HomePage from "./HomePage.jsx";
import TicketConfirmation from './TicketConfirmation.jsx'; // Make sure the file name matches
import FoundConnectionList from "./FoundConnections.jsx";
import complaints from "./ComplaintFiles/complaints";
import ComplaintPage from "./ComplaintFiles/ComplaintPageUser";
import AccountService from './AccountService.jsx';
import StatisticsService from './StatisticsService.jsx';
import ChangeEmail from './ChangeEmail.jsx';
import ChangePassword from './ChangePassword.jsx';
import AfterDeleteScreen from './AfterDeleteScreen.jsx';

const root = createRoot(document.getElementById('root')); // Use createRoot to initialize the root
root.render(
  <React.StrictMode>
    <Router>
    <Navbar/>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/home" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegistrationPage />} />
        <Route path="/account" element={<AccountPanel />}>
          <Route path="tickets" element={<TicketList tickets={tickets} />} />
          <Route path="info" element={<AccountService />} />
          <Route path="statistics" element={<StatisticsService />} />
        </Route>
        <Route path="/ChangeEmail" element={<ChangeEmail />} />
        <Route path="/ChangePassword" element={<ChangePassword />} />
        <Route path="/ticketConfirmation" element={<TicketConfirmation />} />
        <Route path="/FoundConnections" element={<FoundConnectionList />} />
        <Route path="/complaints" element={ <ComplaintPage complaints={complaints} />} />
        <Route path="/after-delete" element={ <AfterDeleteScreen />} />
      </Routes>
    </Router>
  </React.StrictMode>
);
