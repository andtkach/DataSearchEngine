import { useContext } from "react";
import { SpeakerModalContext } from "../contexts/SpeakerModalContext";

export default function EditSpeakerDialog({
  id,
  firstName,
  lastName,
  email,
  country,
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
        setModalShow(true);
      }}
      className="btn btn-accent btn-sm"
    >
      <i className="fa fa-edit"></i>
      {" Edit "}
    </button>
  );
}
