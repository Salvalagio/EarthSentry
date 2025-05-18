import React, { useMemo, useState } from "react";
import { ThemeProvider, createTheme, CssBaseline, Button, AppBar, Toolbar, Typography, Container, Box } from "@mui/material";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import FeedPage from "./pages/Feed";
import { LuSunMedium } from "react-icons/lu";
import { FaMoon } from "react-icons/fa";

const SunIcon = LuSunMedium as unknown as React.FC<{ size?: number }>;
const MoonIcon = FaMoon as unknown as React.FC<{ size?: number }>;

const App = () => {
  const [darkMode, setDarkMode] = useState(false);

  const theme = useMemo(
    () =>
      createTheme({
        palette: {
          mode: darkMode ? "dark" : "light",
          primary: {
            main: "#4caf50",
          },
          secondary: {
            main: "#ff9800",
          },
        },
        typography: {
          fontFamily: "Roboto, sans-serif",
        },
      }),
    [darkMode]
  );

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <AppBar position="static">
          <Toolbar sx={{ position: "relative", justifyContent: "space-between" }}>
            <Typography variant="h6" sx={{ zIndex: 1 }}>
              EarthSentry
            </Typography>
            <Typography
              variant="h6"
              sx={{
                position: "absolute",
                left: "50%",
                transform: "translateX(-50%)",
                textAlign: "center",
              }}>
              Feed
            </Typography>
            <Box sx={{ zIndex: 1 }}>
              <Button color="inherit" onClick={() => setDarkMode(!darkMode)} sx={{
                position: "relative",
                width: 40,
                height: 40,
              }} >
                <Box
                  sx={{
                    position: "absolute",
                    top: 0,
                    left: 0,
                    width: "100%",
                    height: "100%",
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    transition: "opacity 1s ease, transform 1s ease",
                    opacity: darkMode ? 1 : 0,
                    transform: darkMode ? "scale(1)" : "scale(0.8)",
                  }}
                >
                  <SunIcon size={24} />
                </Box>
                <Box
                  sx={{
                    position: "absolute",
                    top: 0,
                    left: 0,
                    width: "100%",
                    height: "100%",
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    transition: "opacity 1s ease, transform 1s ease",
                    opacity: darkMode ? 0 : 1,
                    transform: darkMode ? "scale(0.8)" : "scale(1)",
                  }}
                >
                  <MoonIcon size={24} />
                </Box>
              </Button>
            </Box>
          </Toolbar>
        </AppBar>
        <Container sx={{ mt: 4 }}>
          <Routes>
            <Route path="/" element={<FeedPage />} />
          </Routes>
        </Container>
      </Router>
    </ThemeProvider>
  );
};

export default App;