import React, { useState, useEffect } from "react";
import axios from "axios";

const RankingService = () => {
  const [userRankings, setUserRankings] = useState(null);

  const userID = "1";
  useEffect(() => {
    const fetchUserRankings = async () => {
      try {
        const response = await axios.get(
          `https://localhost:5001/api/Ranking/${userID}`
        );
        const rankingsData = response.data;
        setUserRankings(rankingsData);
      } catch (error) {
        console.error("Wystąpił błąd:", error);
      }
    };

    fetchUserRankings();
  }, []);

  const getRankingName = (rankingId) => {
    switch (rankingId) {
      case 1:
        return "Ranking punktów";
      case 2:
        return "Ranking liczby podróży";
      case 3:
        return "Ranking liczby połączeń";
      case 4:
        return "Ranking najdłuższego czasu podróży";
      case 5:
        return "Ranking przejechanych kilometrów";
      default:
        return "Nieznany ranking";
    }
  };

  return (
    <>
      {userRankings ? (
        <div className="user-rankings">
          {userRankings.map((ranking, index) => (
            <div key={index}>
              <p>
                <strong>Nazwa rankingu:</strong>{" "}
                {getRankingName(ranking.rankingId)}
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
