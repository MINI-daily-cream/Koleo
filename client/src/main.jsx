import React from 'react';
import { Routes, BrowserRouter as Router, Route } from 'react-router-dom';
import { createRoot } from 'react-dom/client'; // Correct import for React 18
import './App.css';
import './index.css';
import LoginPage from './LoginPage';
import RegistrationPage from './RegistrationPage';
import TicketList from "./TicketList";
import tickets from "./tickets.jsx";
import Navbar from "./Navbar.jsx";
import AccountPanel from "./AccountPanel.jsx";
import HomePage from "./HomePage.jsx";
import TicketConfirmation from './TicketConfirmation'; // Make sure the file name matches
import FoundConnectionList from "./FoundConnections";

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
        <Route path="/account" element={<AccountPanel />} />
        <Route path="/tickets" element={<TicketList tickets={tickets} />} />
        <Route path="/ticketConfirmation" element={<TicketConfirmation />} />
        <Route path="/FoundConnections" element={<FoundConnectionList />} />
      </Routes>
    </Router>
  </React.StrictMode>
);
