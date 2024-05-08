import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";

const ChangeEmail = () => {
  const [email, setEmail] = useState("");
  const [userData, setUserData] = useState(null);
  const id = 3;

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
        setUserData(userData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserData();
  }, []);

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(`https://localhost:5001/api/Account/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: userData.name,
          surname: userData.surname,
          email: email,
        }),
      });

      if (!response.ok) {
        throw new Error("Błąd podczas zmiany adresu email");
      }
    } catch (error) {
      console.error("Wystąpił błąd:", error);
    }
  };

  return (
    <div className="form">
      <h1>Zmień email</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nowy adres email:</label>
          <input
            type="email"
            placeholder="Nowy adres email"
            value={email}
            onChange={handleEmailChange}
            required
          />
        </div>

        <button type="submit">Zatwierdź</button>
      </form>

      <Link to="/">Wróć do strony głównej</Link>
    </div>
  );
};

export default ChangeEmail;
