import React, { useState } from "react";
import {
  Box,
  Button,
  CircularProgress,
  Container,
  TextField,
  Typography,
} from "@mui/material";
import { postUserLogin } from "../services/LoginService";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { setUserId, setUserImage } = useAuth();

  const handleLogin = async () => {
    try {
      setLoading(true);
      setError("");

      const response = await postUserLogin({
        username,
        password,
        userImage: null,
        userId: null,
        message: null
      });

      setUserId(response.userId);
      setUserImage(response.userImage);
      navigate("/feed");
    } catch (err: any) {
      setError("Login failed. Check your credentials.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box
      sx={{
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Box
        component="img"
        src="tree.jpeg"
        alt="Tree"
        sx={{ width: "100%", maxHeight: 300, objectFit: "cover" }}
      />
      <Container
        maxWidth="xs"
        sx={{
          display: "flex",
          flexDirection: "column",
          gap: 2,
          mt: 2,
        }}
      >
        <Typography variant="h5" textAlign="center" fontWeight={600}>
          Welcome to EarthSentry
        </Typography>

        <TextField
          label="Username"
          variant="outlined"
          fullWidth
          InputProps={{ sx: { borderRadius: 2 } }}
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />

        <TextField
          label="Password"
          variant="outlined"
          type="password"
          fullWidth
          InputProps={{ sx: { borderRadius: 2 } }}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        {error && <Typography color="error">{error}</Typography>}

        <Button
          variant="contained"
          fullWidth
          onClick={handleLogin}
          disabled={loading}
          sx={{
            backgroundColor: "#007bff",
            color: "#fff",
            py: 1.5,
            borderRadius: 2,
            textTransform: "none",
            fontWeight: "bold",
            "&:hover": {
              backgroundColor: "#006be6",
            },
          }}
        >
          {loading ? <CircularProgress size={24} color="inherit" /> : "Log In"}
        </Button>
      </Container>
    </Box>
  );
};

export default LoginPage;