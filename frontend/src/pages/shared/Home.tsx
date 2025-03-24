import React from "react";
import { useNavigate } from "react-router-dom";

const Home: React.FC = () => {
  const navigate = useNavigate();

  const handleReportIssuesClick = () => {
    navigate("/reportissue");
  };

  return (
    <div>
      <h1>Welcome to Home Page</h1>
      <button onClick={handleReportIssuesClick}>Report an issue (test)</button>
    </div>
  );
};

export default Home;
