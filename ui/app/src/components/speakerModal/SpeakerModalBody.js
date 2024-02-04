import { useContext } from "react";
import { SpeakerModalContext } from "../contexts/SpeakerModalContext";

export default function NotesModalBody() {
  const {
    modalSpeakerId,
    modalSpeakerFirstName,
    setModalSpeakerFirstName,
    modalSpeakerLastName,
    setModalSpeakerLastName,
    modalSpeakerCountry,
    setModalSpeakerCountry,
    modalSpeakerEmail,
    setModalSpeakerEmail,
    modalSpeakerBio,
    setModalSpeakerBio,
  } = useContext(SpeakerModalContext);
  
  return (
    <div className="modal-body">
      <div className="notes-box">
        <div className="notes-content">
          <form>
            <div className="row">
              <div className="col-md-12">
                <div className="note-title">
                  <label>Person Id:</label>
                  <span>{modalSpeakerId}</span>
                </div>
              </div>
              <div className="col-md-12">
                <div className="note-title">
                  <label>First Name</label>
                  <input
                    value={modalSpeakerFirstName}
                    onChange={(event) => {
                      setModalSpeakerFirstName(event.target.value);
                    }}
                    type="text"
                    className="form-control"
                    placeholder="First Name"
                  />
                </div>
              </div>
              <div className="col-md-12">
                <div className="note-title">
                  <label>Last Name</label>
                  <input
                    value={modalSpeakerLastName}
                    onChange={(event) => {
                      setModalSpeakerLastName(event.target.value);
                    }}
                    type="text"
                    className="form-control"
                    placeholder="Last Name"
                  />
                </div>
              </div>
              <div className="col-md-12">
                <div className="note-title">
                  <label>Email</label>
                  <input
                    value={modalSpeakerEmail}
                    onChange={(event) => {
                      setModalSpeakerEmail(event.target.value);
                    }}
                    type="text"
                    className="form-control"
                    placeholder="Email"
                  />
                </div>
              </div>
              <div className="col-md-12">
                <div className="note-title">
                  <label>Country:</label>
                  <input
                    value={modalSpeakerCountry}
                    onChange={(event) => {
                      setModalSpeakerCountry(event.target.value);
                    }}
                    type="text"
                    className="form-control"
                    placeholder="Country"
                  />
                </div>
              </div>

              <div className="col-md-12">
                <div className="note-title">
                  <label>Bio:</label>
                  <textarea
                    value={modalSpeakerBio}
                    onChange={(event) => {
                      setModalSpeakerBio(event.target.value);
                    }}
                    className="form-control"
                    rows={4}
                    placeholder="Bio"></textarea>                  
                </div>
              </div>

            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
