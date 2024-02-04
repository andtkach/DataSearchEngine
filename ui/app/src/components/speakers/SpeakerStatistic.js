import React, { useContext } from "react";
import { SpeakersDataContext } from "../contexts/SpeakersDataContext";

export default function SpeakerStatistic() {

  const { total } = useContext(SpeakersDataContext);
  
  return (
    <div>    
          <h5>DB-{total}</h5>          
    </div>
  );
}
