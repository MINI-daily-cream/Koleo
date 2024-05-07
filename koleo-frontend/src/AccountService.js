import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import DeleteAccountButton from "./DeleteButton"; // Import the DeleteAccountButton component

const AccountService = () => {
  const [userInfo, setUserInfo] = useState(null);
  const id = "C4630E12-DEE8-411E-AF44-E3CA970455CE";

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const response = await fetch(
          `https://localhost:5001/api/Account/${id}`
        );
        if (!response.ok) {
          throw new Error("Błąd pobierania danych użytkownika");
        }
        const userData = await response.json();
        setUserInfo(userData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserData();
  }, []);

  return (
    <div className="account-panel">
      <h1>Moje Konto</h1>
      <div className="account-panel-inside">
        <div className="sidenav">
          <Link to="/AccountService">Dane użytkownika</Link>
          {/* <hr class="solid"></hr> */}
          <a href="#">Bilety</a>
          <Link to="/Statistics">Statystyki</Link>
          <a href="#">Osiągnięcia</a>
        </div>
        {userInfo ? (
          <div className="user-info">
            <p>
              <strong>Imię:</strong> {userInfo.name}
            </p>
            <p>
              <strong>Nazwisko:</strong> {userInfo.surname}
            </p>
            <p>
              <strong>Adres email:</strong> {userInfo.email}
            </p>
            <Link to="/ChangeEmail">Zmień email</Link>
            <Link to="/ChangePassword">Zmień hasło</Link>
            <DeleteAccountButton />
          </div>
        ) : (
          <p>Ładowanie danych użytkownika...</p>
        )}
      </div>
    </div>
  );
};

export default AccountService;
