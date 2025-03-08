import React from "react";
import { useParams } from "react-router-dom";

// ejemplo para el react router 


const UserProfile: React.FC = () => {
  const { id } = useParams<Record<string, string | undefined>>();

  return <h1>Perfil del usuario {id}</h1>;
};

export default UserProfile;