import { useEffect, useMemo, useState } from "react";
import { ThemeProvider, createTheme, CssBaseline, Container } from "@mui/material";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FeedPage from "./pages/Feed";
import LoginPage from "./pages/LoginPage";
import PostDetailPage from "./pages/PostDetailPage";
import ProfilePage from "./pages/ProfilePage";
import { AuthProvider } from "./contexts/AuthContext";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

const App = () => {
  const [darkMode, setDarkMode] = useState(() => {
    const savedMode = localStorage.getItem("darkMode");
    return savedMode ? JSON.parse(savedMode) : false;
  });

  useEffect(() => {
    localStorage.setItem("darkMode", JSON.stringify(darkMode));
  }, [darkMode]);

  const theme = useMemo(
    () =>
      createTheme({
        palette: {
          mode: darkMode ? "dark" : "light",
          primary: {
            main: "#FFFFFF",
          },
          secondary: {
            main: "#ff9800",
          }
        },
        typography: {
          fontFamily: "Source Serif 4, sans-serif",
        },
      }),
    [darkMode]
  );

  return (
    <AuthProvider>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Container sx={{ mt: 4 }}>
            <Routes>
              <Route path="*" element={<LoginPage />}></Route>
              <Route path="/feed" element={<FeedPage darkMode={darkMode} onToggle={() => setDarkMode(!darkMode)} />} />
              <Route path="/postDetail" element={<PostDetailPage />}></Route>
              <Route path="/profilePage" element={<ProfilePage onBack={function (): void {
                throw new Error("Function not implemented.");
              }} onEditPhoto={function (): void {
                throw new Error("Function not implemented.");
              }} onLogout={function (): void {
                throw new Error("Function not implemented.");
              }} email={"teste@teste.com"} profileImage={"12345"} />} />
            </Routes>
            <ToastContainer position="bottom-center" autoClose={2000} />
          </Container>
        </Router>
      </ThemeProvider>
    </AuthProvider>
  );
};

export default App;