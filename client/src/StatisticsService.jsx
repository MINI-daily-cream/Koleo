import React, { useState, useEffect } from "react";
import axios from "axios";

const StatisticsService = () => {
  const [userAchievements, setUserAchievements] = useState(null);

  const id = 1;
  useEffect(() => {
    const fetchUserAchievements = async () => {
      try {
        const response = await axios.get(
          `https://localhost:5001/api/Statistics/${id}`
        );
        const achievementsData = response.data;
        setUserAchievements(achievementsData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserAchievements();
  }, []);

  return (
    <>
      {userAchievements ? (
        <div className="user-achievements">
          <p>
            <strong>Przejechane kilometry:</strong> {userAchievements.kmNumber}
          </p>
          <p>
            <strong>Liczba podróży pociągiem:</strong>{" "}
            {userAchievements.trainNumber}
          </p>
          <p>
            <strong>Liczba połączeń:</strong>{" "}
            {userAchievements.connectionsNumber}
          </p>
          <p>
            <strong>Najdłuższy czas podróży:</strong>{" "}
            {userAchievements.longestConnectionTime}
          </p>
          <p>
            <strong>Punkty:</strong> {userAchievements.points}
          </p>
        </div>
      ) : (
        <p>Ładowanie osiągnięć użytkownika...</p>
      )}
    </>
  );
};

export default StatisticsService;
