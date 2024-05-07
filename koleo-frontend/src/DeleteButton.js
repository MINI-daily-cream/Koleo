import React from "react";

const DeleteAccountButton = () => {
  const handleDeleteAccount = async () => {
    const id = "C4630E12-DEE8-411E-AF44-E3CA970455CE"; // Assuming you have the ID of the account you want to delete

    try {
      const response = await fetch(`https://localhost:5001/api/Account/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Failed to delete account");
      }

      console.log("Account deleted successfully");
      // Optionally, you can perform additional actions here after successful deletion
    } catch (error) {
      console.error("Error deleting account:", error);
    }
  };

  return (
    <button
      onClick={handleDeleteAccount}
      style={{
        backgroundColor: "transparent",
        color: "blue",
        border: "none",
        textDecoration: "underline",
        cursor: "pointer",
      }}
    >
      Delete Account
    </button>
  );
};

export default DeleteAccountButton;
