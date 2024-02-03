import React, { createContext, useState } from "react";

export const SpeakerMenuContext = createContext({
  searchText: "",
  setSearchText: () => {},
});
export const SpeakerMenuProvider = ({ children }) => {
  const [searchText, setSearchText] = useState("");
  const value = {
    searchText,
    setSearchText,
  };
  return (
    <SpeakerMenuContext.Provider value={value}>
      {children}
    </SpeakerMenuContext.Provider>
  );
};
