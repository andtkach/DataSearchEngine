import React, { createContext } from "react";
import useSpeakersData from "../hooks/useSpeakersData";

export const SpeakersDataContext = createContext({
  speakerList: [],
  createSpeaker: () => {},
  updateSpeaker: () => {},
  deleteSpeaker: () => {},
  searchSpeaker: () => {},
  loadingStatus: "",
});

export const SpeakersDataProvider = ({ children }) => {
  const {
    speakerList,
    createSpeaker,
    updateSpeaker,
    deleteSpeaker,
    loadingStatus,
    searchSpeaker,
  } = useSpeakersData();

  const value = {
    speakerList,
    createSpeaker,
    updateSpeaker,
    deleteSpeaker,
    loadingStatus,
    searchSpeaker,
  };

  return (
    <SpeakersDataContext.Provider value={value}>
      {children}
    </SpeakersDataContext.Provider>
  );
};
