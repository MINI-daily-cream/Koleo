import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import apiBaseUrl from "./config";

const AfterDeleteScreen = () => {
  const navigate = useNavigate();

  return (
    <div className="form">
      <h1>Konto zostało usunięte.<br />
      Możesz dalej kupować bilety jako niezalogowany użytkownik lub stworzyć nowe konto.</h1>
      <Link to="/" className="after-delete-link">Wróć do strony głównej</Link>
    </div>
  );
};

export default AfterDeleteScreen;
