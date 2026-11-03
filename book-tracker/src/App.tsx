import './App.css'
import {Route, Routes} from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import NavBar from "./components/NavBar.tsx";
import BookDetailsPage from "./pages/BookDetailsPage.tsx";

function App() {

  return (
      <>
        <NavBar />
        <Routes>
          <Route path="/" element={<HomePage />}/>
          <Route path="/about" element={<AboutPage />}/>
          <Route path="/book/:bookId" element={<BookDetailsPage  />}/>
          {/*<Route path="/book/:bookId" element={<BookDetailsPage  />}/>*/}
        </Routes>
      </>

  )
}

export default App
