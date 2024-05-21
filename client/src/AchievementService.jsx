import React, { useState, useEffect } from "react";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import apiBaseUrl from "./config";

const AchievementsPage = () => {
  const [achievements, setAchievements] = useState([]);
  const [hoveredAchievement, setHoveredAchievement] = useState(null);
  const userId = localStorage.getItem("id");
  const jwtToken = localStorage.getItem("jwtToken");

  useEffect(() => {
    const fetchAchievements = async () => {
      try {
        const response = await axios.get(
          `${apiBaseUrl}/api/Achievement/${userId}`,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: `Bearer ${jwtToken}`,
            },
          }
        );
        setAchievements(response.data);
      } catch (error) {
        if (error.response && error.response.status === 400) {
          console.error("Bad request: User exists");
        } else {
          console.error("An error occurred:", error.message);
        }
      }
    };
    fetchAchievements();
  }, [userId, jwtToken]);

  const handleOnMouseOver = (achievement) => {
    setHoveredAchievement(achievement);
  };

  const handleOnMouseOut = () => {
    setHoveredAchievement(null);
  };

  return (
    <div className="achievements-page">
      <h1>Achievements</h1>
      <div className="achievements-list">
        {achievements.map((achievement, index) => (
          <div
            key={index}
            className="achievement-icon"
            onMouseOver={() => handleOnMouseOver(achievement)}
            onMouseOut={handleOnMouseOut}
          >
            <FontAwesomeIcon icon={faStar} size="2x" />
            {hoveredAchievement === achievement && (
              <div className="achievement-tooltip">{achievement.name}</div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};

export default AchievementsPage;
