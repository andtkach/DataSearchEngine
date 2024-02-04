import React, { createContext } from "react";
import useSpeakersData from "../hooks/useSpeakersData";

export const SpeakersDataContext = createContext({
  speakerList: [],
  createSpeaker: () => {},
  updateSpeaker: () => {},
  deleteSpeaker: () => {},
  searchSpeaker: () => {},
  loadingStatus: "",
  total: 0,
});

export const SpeakersDataProvider = ({ children }) => {
  const {
    speakerList,
    createSpeaker,
    updateSpeaker,
    deleteSpeaker,
    loadingStatus,
    searchSpeaker,
    total,
  } = useSpeakersData();

  const value = {
    speakerList,
    createSpeaker,
    updateSpeaker,
    deleteSpeaker,
    loadingStatus,
    searchSpeaker,
    total,
  };

  return (
    <SpeakersDataContext.Provider value={value}>
      {children}
    </SpeakersDataContext.Provider>
  );
};
