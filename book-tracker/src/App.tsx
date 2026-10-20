import './App.css'
import {Route, Routes} from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import NavBar from "./components/NavBar.tsx";

function App() {

  return (
      <>
        <NavBar />
        <Routes>
          <Route path="/" element={<HomePage />}/>
          <Route path="/about" element={<AboutPage />}/>
        </Routes>
      </>

  )
}

export default App
