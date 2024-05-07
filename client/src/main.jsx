import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import AccountPage from './AccountPage.jsx'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

// why this doesnt work
ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Router>
      <Routes>
        <Route path='/' element={<App />} />
        <Route path='account' element={<AccountPage/>} />
      </Routes>
    </Router>
  </React.StrictMode>,
);
