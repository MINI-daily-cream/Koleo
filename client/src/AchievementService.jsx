import React, { useState, useEffect } from "react";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStar } from "@fortawesome/free-solid-svg-icons";

const AchievementsPage = () => {
  const [achievements, setAchievements] = useState([]);
  const [hoveredAchievement, setHoveredAchievement] = useState(null);
  const userID = "1";

  useEffect(() => {
    const fetchAchievements = async () => {
      try {
        const response = await axios.get(
          `https://localhost:5001/api/Achievement/${userID}`
        );
        setAchievements(response.data);
      } catch (error) {
        console.error("Error fetching achievements:", error);
      }
    };

    fetchAchievements();
  }, []);

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
