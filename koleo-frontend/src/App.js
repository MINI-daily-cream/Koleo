import React from "react";
import {Routes, BrowserRouter as Router, Route } from 'react-router-dom'
import './App.css';
import LoginPage from './LoginPage';
import RegistrationPage from './RegistrationPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegistrationPage />} />
      </Routes>
    </Router>
  );
}

export default App;
