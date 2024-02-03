import { useState } from "react";

function useSpeakerModal() {
  const [modalShow, setModalShow] = useState(false);

  const [modalSpeakerId, setModalSpeakerId] = useState(0);
  const [modalSpeakerFirstName, setModalSpeakerFirstName] = useState("");
  const [modalSpeakerLastName, setModalSpeakerLastName] = useState("");
  const [modalSpeakerCountry, setModalSpeakerCountry] = useState("");
  const [modalSpeakerEmail, setModalSpeakerEmail] = useState("");
  const [modalSpeakerBio, setModalSpeakerBio] = useState("");

  return {
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
}

export default useSpeakerModal;
