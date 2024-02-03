import React, { createContext } from "react";
import useSpeakerModal from "../hooks/useSpeakerModal";

export const SpeakerModalContext = createContext({
  modalShow: false,
  setModalShow: () => {},
  modalSpeakerId: "",
  setModalSpeakerId: () => {},
  modalSpeakerFirstName: "",
  setModalSpeakerFirstName: () => {},
  modalSpeakerLastName: "",
  setModalSpeakerLastName: () => {},
  modalSpeakerCountry: "",
  setModalSpeakerCountry: () => {},
  modalSpeakerBio: "",
  setModalSpeakerBio: () => {},
});

export const SpeakerModalProvider = ({ children }) => {
  const {
    modalShow,
    setModalShow,
    modalSpeakerId,
    setModalSpeakerId,
    modalSpeakerFirstName,
    setModalSpeakerFirstName,
    modalSpeakerLastName,
    setModalSpeakerLastName,
    modalSpeakerEmail,
    setModalSpeakerEmail,
    modalSpeakerCountry,
    setModalSpeakerCountry,
    modalSpeakerBio,
    setModalSpeakerBio,
  } = useSpeakerModal();

  const value = {
    modalShow,
    setModalShow,
    modalSpeakerId,
    setModalSpeakerId,
    modalSpeakerFirstName,
    setModalSpeakerFirstName,
    modalSpeakerLastName,
    setModalSpeakerLastName,
    modalSpeakerEmail,
    setModalSpeakerEmail,
    modalSpeakerCountry,
    setModalSpeakerCountry,
    modalSpeakerBio,
    setModalSpeakerBio,
  };

  return (
    <SpeakerModalContext.Provider value={value}>
      {children}
    </SpeakerModalContext.Provider>
  );
};
