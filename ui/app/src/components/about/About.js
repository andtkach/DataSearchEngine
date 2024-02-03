import { useContext } from "react";
import { ThemeContext } from "../contexts/ThemeContext";

export default function About() {
  const { darkTheme } = useContext(ThemeContext);
  return (
    <div className={darkTheme ? "theme-dark" : "theme-light"}>
      <div className="container">
        <div id="content" className="content-wrapper">
          <div className="about">
            <h1 className="hTitle">About</h1>
            <div className="card border-0 p-3">
              <div className="card-body">
                <p className="card-text">
                  Data search engine is a web application that allows users to manupulate and then to search for a data set by entering a search term.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
