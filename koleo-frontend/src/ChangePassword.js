import { faPassport } from "@fortawesome/free-solid-svg-icons";
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const ChangePassword = () => {
  const [oldpassword, setOldPassword] = useState("");

  const [newpassword, setNewPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [userData, setUserData] = useState(null);
  const id = "C4630E12-DEE8-411E-AF44-E3CA970455CE";

  const handleOldPasswordChange = (e) => {
    setOldPassword(e.target.value);
  };

  const handleNewPasswordChange = (e) => {
    const newPassword = e.target.value;
    setNewPassword(newPassword);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(
        `https://localhost:5001/api/Account/${id}/ChangePassword?newPassword=${newpassword}&oldPassword=${oldpassword}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        throw new Error("Błąd podczas zmiany hasła");
      }

      console.log("Password changed successfully"); // Log success message
    } catch (error) {
      console.error("Wystąpił błąd:", error);
    }
  };

  return (
    <div className="form">
      <h1>Zmień hasło</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Stare hasło:</label>
          <input
            type="password"
            id="password"
            value={oldpassword}
            onChange={handleOldPasswordChange}
            required
          />
        </div>
        <div>
          <label htmlFor="password">Nowe hasło:</label>
          <input
            type="password"
            id="password"
            value={newpassword}
            onChange={handleNewPasswordChange}
            required
          />
          <button type="submit">Zatwierdź</button>
        </div>
      </form>
    </div>
  );
};

export default ChangePassword;
