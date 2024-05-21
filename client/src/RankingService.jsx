import React, { useState, useEffect } from "react";
import axios from "axios";
import apiBaseUrl from "./config";

const RankingService = () => {
  const [userRankings, setUserRankings] = useState(null);

  const userId = localStorage.getItem("id");
  const jwtToken = localStorage.getItem("jwtToken");

  useEffect(() => {
    const fetchUserRankings = async () => {
      try {
        const response = await axios.get(`${apiBaseUrl}/api/Ranking/${userId}`);
        const rankingsData = response.data;
        setUserRankings(rankingsData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserRankings();
  }, []);

  return (
    <>
      {userRankings ? (
        <div className="user-rankings">
          {userRankings.map((ranking, index) => (
            <div key={index}>
              <p>
                <strong>Nazwa rankingu:</strong> {ranking.rankingName}
              </p>
              <p>
                <strong>Pozycja:</strong> {ranking.position}
              </p>
            </div>
          ))}
        </div>
      ) : (
        <p>Ładowanie pozycji w rankingu...</p>
      )}
    </>
  );
};

export default RankingService;
