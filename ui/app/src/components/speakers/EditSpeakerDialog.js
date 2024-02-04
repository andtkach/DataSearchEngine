import { useContext } from "react";
import { SpeakerModalContext } from "../contexts/SpeakerModalContext";

export default function EditSpeakerDialog({
  id,
  firstName,
  lastName,
  email,
  country,
  bio,
}) {
  const {
    setModalShow,
    modalShow,
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
  } = useContext(SpeakerModalContext);

  return (
    <button
      onClick={(e) => {
        e.preventDefault();
        setModalSpeakerId(id);
        setModalSpeakerFirstName(firstName);
        setModalSpeakerLastName(lastName);
        setModalSpeakerCountry(country);
        setModalSpeakerEmail(email);
        setModalSpeakerBio(bio);
        setModalShow(true);
      }}
      className="btn btn-accent btn-sm"
    >
      <i className="fa fa-edit"></i>
      {" Edit "}
    </button>
  );
}
