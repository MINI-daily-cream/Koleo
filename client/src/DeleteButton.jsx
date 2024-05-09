import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import apiBaseUrl from "./config";

const DeleteAccountButton = () => {
  const navigate = useNavigate();
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));

  async function handleDeleteAccount() {
    console.log("hello")
    const makeRequest = async () => {
      try {
        const response = await axios.delete(`${apiBaseUrl}/api/Account/delete-user/${userId}`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwtToken}`
            }
        });
      } 
      catch(error) {
        if (error === 'Bad request') {
            console.error('user exists');
        } else {
            console.error('An error occurred:', error);
        }
      }
    };
    await makeRequest();
    navigate("/after-delete")
  }

  return (
    <button
      onClick={handleDeleteAccount}
      style={{
        backgroundColor: "transparent",
        color: "blue",
        border: "none",
        textDecoration: "underline",
        cursor: "pointer",
      }}
    >
      Delete Account
    </button>
  );
};

export default DeleteAccountButton;
