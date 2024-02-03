import DeleteSpeakerButton from "./DeleteSpeakerButton";
import EditSpeakerDialog from "./EditSpeakerDialog";
import FavoriteSpeakerToggle from "./FavoriteSpeakerToggle";
import SpeakerImageToggleOnScroll from "./SpeakerImageToggleOnScroll";
import { SpeakerModalProvider } from "../contexts/SpeakerModalContext";
import SpeakerModal from "../speakerModal/SpeakerModal";

export default function SpeakerDetail({ speakerRec, showDetails, searchTerm }) {
  const { setRoute } = {
    setRoute: (route) => {
      window.location.href = route;
    },
  };

  const needhl = (data) => {
    
    if (!searchTerm) return false;
    if (!data) return false;  

    if (data.toLowerCase().includes(searchTerm.toLowerCase()))
      return true;

    return false;
  }

  return (
    <SpeakerModalProvider>
      {speakerRec && <SpeakerModal />}
      <div className="col-xl-6 col-md-12">
        <div className="card border-0">
          <div className="row g-0">
            
            <div className="col-12 d-flex flex-column flex-nowrap">
              <div className="card-body">
                <div className="speaker-action d-flex">
                  <div className="favoriteToggleWrapper">                    
                  </div>

                  <div className="modifyWrapper">
                    <EditSpeakerDialog {...speakerRec} />
                    <DeleteSpeakerButton id={speakerRec.id} />
                  </div>
                </div>
                <h4 className={needhl(speakerRec.firstName) || needhl(speakerRec.lastName)
                    ? "mb-1 background-text-highlight card-title"
                    : "mb-1 card-title"}>
                  <a
                    onClick={() => {
                      setRoute(`/speaker/${speakerRec.id}`);
                    }}
                    href="#"
                  >
                    {speakerRec.firstName} {speakerRec.lastName}
                  </a>
                </h4>

                <p className={needhl(speakerRec.bio)
                    ? "mb-1 background-text-highlight card-text"
                    : "mb-1 card-text"}>{speakerRec.bio}</p>
                
              </div>

              <div className="card-footer text-muted d-flex flex-wrap justify-content-between align-items-center">
                {speakerRec?.country?.length > 0 ? (
                  <small className={needhl(speakerRec.country)
                    ? "mb-1 background-text-highlight"
                    : "mb-1"}>
                    <strong>Country:</strong> {speakerRec.country}
                  </small>
                ) : null}

                {speakerRec.email.length > 0 ? (
                  <small className={needhl(speakerRec.email)
                    ? "mb-1 background-text-highlight"
                    : "mb-1"}>
                    <strong>Email</strong>: {speakerRec.email}
                  </small>
                ) : null}
              </div>
            </div>
          </div>
        </div>
      </div>
    </SpeakerModalProvider>
  );
}
