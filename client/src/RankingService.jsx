import React, { useState, useEffect } from "react";
import axios from "axios";
import apiBaseUrl from "./config";

const RankingService = () => {
  const [rankings, setRankings] = useState([]);
  const [currentRankingIndex, setCurrentRankingIndex] = useState(0);
  const [markedUserId, setMarkedUserId] = useState(
    localStorage.getItem("id").toUpperCase()
  );

  const userId = localStorage.getItem("id");
  const jwtToken = localStorage.getItem("jwtToken");

  useEffect(() => {
    const fetchRankings = async () => {
      try {
        const response = await axios.get(
          `${apiBaseUrl}/api/Ranking/${userId}`,
          {
            headers: {
              Authorization: `Bearer ${jwtToken}`,
            },
          }
        );
        const rankingsData = response.data;
        setRankings(rankingsData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchRankings();
  }, [userId, jwtToken]);

  const handleRankingChange = (index) => {
    setCurrentRankingIndex(index);
  };

  const isMarkedUser = (userId) => {
    return userId.toUpperCase() === markedUserId;
  };

  return (
    <>
      {rankings.length > 0 ? (
        <>
          <div className="ranking-bookmarks">
            {rankings.map((_, index) => (
              <button
                key={index}
                onClick={() => handleRankingChange(index)}
                disabled={index === currentRankingIndex}
                className="ranking-button"
              >
                {rankings[index][0].rankingName}
              </button>
            ))}
          </div>
          <div className="ranking-group">
            <h2>
              Nazwa rankingu: {rankings[currentRankingIndex][0].rankingName}
            </h2>
            <table
              style={{
                borderCollapse: "collapse",
                width: "100%",
                border: "1px solid #ddd",
              }}
            >
              <thead style={{ backgroundColor: "#f2f2f2" }}>
                <tr>
                  <th style={{ border: "1px solid #ddd", padding: "8px" }}>
                    Pozycja
                  </th>
                  <th style={{ border: "1px solid #ddd", padding: "8px" }}>
                    Nazwa użytkownika
                  </th>
                  <th style={{ border: "1px solid #ddd", padding: "8px" }}>
                    Punkty
                  </th>
                </tr>
              </thead>
              <tbody>
                {rankings[currentRankingIndex]
                  .slice()
                  .sort((a, b) => a.position - b.position)
                  .map((ranking, userIndex) => (
                    <tr
                      key={userIndex}
                      style={{
                        backgroundColor:
                          userIndex % 2 === 0 ? "#f9f9f9" : "white",
                        fontWeight: isMarkedUser(ranking.user_Id)
                          ? "bold"
                          : "normal",
                      }}
                    >
                      <td style={{ border: "1px solid #ddd", padding: "8px" }}>
                        {ranking.position}
                      </td>
                      <td style={{ border: "1px solid #ddd", padding: "8px" }}>
                        {ranking.username}
                      </td>
                      <td style={{ border: "1px solid #ddd", padding: "8px" }}>
                        {ranking.points}
                      </td>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        </>
      ) : (
        <p>Ładowanie pozycji w rankingu...</p>
      )}
    </>
  );
};

export default RankingService;
