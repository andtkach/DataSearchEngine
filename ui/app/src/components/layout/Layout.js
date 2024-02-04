import AppMenu from "./AppMenu";
import Speakers from "../speakers/Speakers";
import About from "../about/About";
import Speaker from "../speakers/Speaker";
import { ThemeProvider } from "../contexts/ThemeContext";

export default function Layout({ url }) {
  const speakerId = url.substring(url.lastIndexOf('/' ) + 1);

  return (
    <ThemeProvider>
      <AppMenu />
      {url === "/about" && <About />}
      {url === "/" && <Speakers />}
      {url.startsWith("/speaker/") && <Speaker id={speakerId} />}
    </ThemeProvider>
  );
}
