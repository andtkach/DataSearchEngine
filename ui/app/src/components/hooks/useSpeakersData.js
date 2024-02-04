import useGeneralizedCrudMethods from "./useGeneralizedCrudMethods";

function useSpeakersData() {
  
  const urls = {
    search: "http://localhost:7498/person/search",
    latest: "http://localhost:7498/person/all", // "http://localhost:7498/person/latest",
    mutate: "http://localhost:7298/persons",
    total: "http://localhost:7498/person/count",
  }

  const errorNotificationFn = (error) => {
    console.error("Error From useSpeakersData", error);
  };

  const {
    data,
    error,
    loadingStatus,
    createRecord,
    updateRecord,
    deleteRecord,
    searchRecord,
    total,
  } = useGeneralizedCrudMethods(urls, errorNotificationFn);

  function createSpeaker(speakerRec, callbackDone) {
    createRecord(speakerRec, callbackDone);
  }

  function updateSpeaker(speakerRec, callbackDone) {
    updateRecord(speakerRec, callbackDone);
  }

  function deleteSpeaker(id, callbackDone) {
    deleteRecord(id, callbackDone);
  }

  function searchSpeaker(term, callbackDone) {
    searchRecord(term, callbackDone);
  }

  return {
    speakerList: data,
    loadingStatus,
    error,
    createSpeaker,
    updateSpeaker,
    deleteSpeaker,
    searchSpeaker,
    total,
  };
}

export default useSpeakersData;
