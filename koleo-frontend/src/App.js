import React from "react";
import { Routes, BrowserRouter as Router, Route } from "react-router-dom";
import "./App.css";
import LoginPage from "./LoginPage";
import RegistrationPage from "./RegistrationPage";
import TicketList from "./TicketList";
import tickets from "./tickets.js";
import Navbar from "./Navbar.js";
import AccountPanel from "./AccountPanel.js";
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
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegistrationPage />} />
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

export default App;
