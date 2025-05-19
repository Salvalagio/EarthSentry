import { useEffect, useMemo, useState } from "react";
import { ThemeProvider, createTheme, CssBaseline, Container } from "@mui/material";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FeedPage from "./pages/Feed";
import LoginPage from "./pages/LoginPage";

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
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Container sx={{ mt: 4 }}>
          <Routes>
            <Route path="/" element={<LoginPage />}></Route>
            <Route path="/feed" element={<FeedPage  darkMode={darkMode} onToggle={() => setDarkMode(!darkMode)} />} />
          </Routes>
        </Container>
      </Router>
    </ThemeProvider>
  );
};

export default App;