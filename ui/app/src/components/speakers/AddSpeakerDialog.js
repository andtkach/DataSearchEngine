import { useContext } from "react";
import SpeakerModal from "../speakerModal/SpeakerModal";
import { SpeakerModalContext } from "../contexts/SpeakerModalContext";

export default function AddSpeakerDialog() {
  const {
    setModalShow,
    setModalSpeakerId,
    setModalSpeakerFirstName,
    setModalSpeakerLastName,
    setModalSpeakerEmail,
    setModalSpeakerCountry,
  } = useContext(SpeakerModalContext);

  return (
    <>
      <SpeakerModal modalShow={false} />
      <button
        onClick={() => {
          setModalSpeakerId(0);
          setModalSpeakerFirstName("");
          setModalSpeakerLastName("");
          setModalSpeakerEmail("");
          setModalSpeakerCountry("");
          setModalShow(true);
        }}
        className="btn btn-accent"
      >
        Add Person <i className="fa fa-plus" />
      </button>
    </>
  );
}
