import { useContext } from "react";
import AddSpeakerDialog from "./AddSpeakerDialog";
import { SpeakerMenuContext } from "../contexts/SpeakerMenuContext";
import { SpeakerModalProvider } from "../contexts/SpeakerModalContext";
import SpeakerStatistic from "./SpeakerStatistic";

export default function SpeakerMenu() {
  const {
    searchText,
    setSearchText,
  } = useContext(SpeakerMenuContext);

  return (
    <div
      className="btn-toolbar"
      role="toolbar"
      aria-label="Speaker toolbar filter"
    >
      <div className="toolbar-trigger mb-3">
        <div className="toolbar-search">
          <input
            value={searchText}
            onChange={(event) => {
              setSearchText(event.target.value);
            }}
            type="text"
            className="form-control"
            placeholder="Search"
          />
        </div>
        
        <div className="input-group">
          <SpeakerModalProvider>
            <AddSpeakerDialog />
          </SpeakerModalProvider>
        </div>
        
        <div>          
          <SpeakerStatistic />
        </div>
      </div>
    </div>
  );
}
