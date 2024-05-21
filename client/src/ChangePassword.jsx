import { faPassport } from "@fortawesome/free-solid-svg-icons";
import React, { useState } from "react";
import axios from "axios";
import apiBaseUrl from "./config";

const ChangePassword = () => {
  const [oldpassword, setOldPassword] = useState("");
  const [newpassword, setNewPassword] = useState("");
  const [userId] = useState(localStorage.getItem("id"));
  const [jwtToken] = useState(localStorage.getItem("jwtToken"));

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.put(
        `${apiBaseUrl}/api/Account/admin-request/change_password?userId=${userId}&oldPassword=${oldpassword}&newPassword=${newpassword}`,
        {},
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwtToken}`,
          },
        }
      );

      if (response.data === true) {
        alert("Hasło zostało pomyślnie zmienione");
      } else {
        alert("Nie udało się zmienić hasła");
      }
    } catch (error) {}
  };

  const handleOldPasswordChange = (e) => {
    setOldPassword(e.target.value);
  };

  const handleNewPasswordChange = (e) => {
    setNewPassword(e.target.value);
  };

  return (
    <div className="form">
      <h1>Zmień hasło</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="oldpassword">Stare hasło:</label>
          <input
            type="password"
            id="oldpassword"
            value={oldpassword}
            onChange={handleOldPasswordChange}
            required
          />
        </div>
        <div>
          <label htmlFor="newpassword">Nowe hasło:</label>
          <input
            type="password"
            id="newpassword"
            value={newpassword}
            onChange={handleNewPasswordChange}
            required
          />
        </div>
        <button type="submit">Zatwierdź</button>
      </form>
    </div>
  );
};

export default ChangePassword;
