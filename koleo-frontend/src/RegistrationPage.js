import React, { useState } from "react";
import { Link } from "react-router-dom";

const RegistrationPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    if (confirmPassword && newPassword !== confirmPassword) {
      setPasswordError("Hasła nie pasują do siebie");
    } else {
      setPasswordError("");
    }
  };

  const handleConfirmPasswordChange = (e) => {
    const newPassword = e.target.value;
    setConfirmPassword(newPassword);
    if (newPassword !== password) {
      setPasswordError("Hasła nie pasują do siebie");
    } else {
      setPasswordError("");
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      setPasswordError("Hasła nie pasują do siebie");
      return;
    }
    // Tutaj można dodać logikę rejestracji
    console.log("Email:", email);
    console.log("Password:", password);
    console.log("Confirm Password:", confirmPassword);
  };

  return (
    <div className="form">
      <h1>Zarejestruj się</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Adres e-mail:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={handleEmailChange}
            required
          />
        </div>
        <div>
          <label htmlFor="password">Hasło:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={handlePasswordChange}
            required
          />
        </div>
        <div>
          <label htmlFor="confirmPassword">Powtórz hasło:</label>
          <input
            type="password"
            id="confirmPassword"
            value={confirmPassword}
            onChange={handleConfirmPasswordChange}
            required
          />
          {passwordError && (
            <p
              className="password-error"
              style={{ color: "red", fontSize: 30 }}
            >
              {passwordError}
            </p>
          )}
        </div>
        <p className="bottom-text" disabled>
          Masz już konto? <Link to="/login">Zaloguj się</Link>
        </p>
        {/* <button type="submit">Zarejestruj się</button> */}
        <Link to="/account">
          <button type="submit">Zarejestruj się</button>
        </Link>
      </form>
    </div>
  );
};

export default RegistrationPage;
