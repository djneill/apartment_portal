import Login from './pages/shared/Login';
import { BrowserRouter, Routes, Route } from "react-router";
import Home from './pages/shared/Home';
import UserProfile from './components/UserProfile';
import './App.css'
import FormDemo from './pages/FormDemo';
import ReportIssue from './pages/guest/ReportIssue';
import Layout from './components/Layout'; 

function App() {

  return (
    <BrowserRouter>
      <Routes>
      <Route path="/" element={<Login />} />
          <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/reportissue" element={<ReportIssue />} />
          <Route path="/users/:id" element={<UserProfile />} />
          <Route path="/formdemo" element={<FormDemo />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
