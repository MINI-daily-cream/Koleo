import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";

const StatisticsService = () => {
  const [userAchievements, setUserAchievements] = useState({
    KmNumber: 1500,
    TrainNumber: 25,
    ConnectionsNumber: 10,
    LongestConnectionTime: "4h 30min",
    Points: 500,
  });
  /*   const id = 3;
  useEffect(() => {
    const fetchUserAchievements = async () => {
      try {
        const response = await fetch(
          `https://localhost:5001/api/Account/Achievements/${id}`
        );
        if (!response.ok) {
          throw new Error("Błąd pobierania osiągnięć użytkownika");
        }
        const achievementsData = await response.json();
        setUserAchievements(achievementsData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserAchievements();
  }, []); */
  return (
    <>
      {userAchievements ? (
        <div className="user-achievements">
          <p>
            <strong>Przejechane kilometry:</strong>{" "}
            {userAchievements.KmNumber}
          </p>
          <p>
            <strong>Liczba podróży pociągiem:</strong>{" "}
            {userAchievements.TrainNumber}
          </p>
          <p>
            <strong>Liczba połączeń:</strong>{" "}
            {userAchievements.ConnectionsNumber}
          </p>
          <p>
            <strong>Najdłuższy czas podróży:</strong>{" "}
            {userAchievements.LongestConnectionTime}
          </p>
          <p>
            <strong>Punkty:</strong> {userAchievements.Points}
          </p>
        </div>
      ) : (
        <p>Ładowanie osiągnięć użytkownika...</p>
      )}
    </>
  );
};

export default StatisticsService;
