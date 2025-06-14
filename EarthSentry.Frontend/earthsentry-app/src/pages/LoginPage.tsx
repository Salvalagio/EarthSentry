import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  CircularProgress,
  Container,
  TextField,
  Typography,
} from "@mui/material";
import { postUserLogin, postUserRegister } from "../services/LoginService";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import { usePostContext } from "../contexts/PostContext";

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPasswordState] = useState("");
  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(false);
  const [newUserRegistering, setNewUserRegistering] = useState(false);
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { setUserId, setUserImage, actionLogoutUser, setPassword } = useAuth();
  const { setPostId } = usePostContext();

  useEffect(() => {
    actionLogoutUser();
  });

  const handleLogin = async () => {
    try {
      setLoading(true);
      setError("");

      const response = await postUserLogin({
        username,
        password,
        email: null,
        imageUrl: null,
        userId: null,
        message: null
      });

      if (!response) {
        setError("Registration failed. Please try again.");
        return;
      }

      setUserId(response.userId);
      setUserImage(response.imageUrl);
      setPassword(password);
      setPostId(0);
      navigate("/feed");
    } catch (err: any) {
      setError("Login failed. Check your credentials.");
    } finally {
      setLoading(false);
    }
  };

  const onRegister = async () => {
    try {
      setLoading(true);
      setError("");

      const response = await postUserRegister({
        username,
        password,
        email,
        imageUrl: null,
        userId: null,
        message: null
      });

      if (!response) {
        setError("Registration failed. Please try again.");
        return;
      }

      setUserId(response.userId);
      setUserImage(response.imageUrl);
      setPassword(password);
      setPostId(0);
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
      onKeyDown={(e) => {
        if (e.key === "Enter") {
          newUserRegistering ? onRegister() : handleLogin();
        }
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

        {
          newUserRegistering &&
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            InputProps={{ sx: { borderRadius: 2 } }}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        }

        <TextField
          label="Password"
          variant="outlined"
          type="password"
          fullWidth
          InputProps={{ sx: { borderRadius: 2 } }}
          value={password}
          onChange={(e) => setPasswordState(e.target.value)}
        />

        {error && <Typography color="error">{error}</Typography>}

        <Button
          variant="contained"
          fullWidth
          onClick={() => {newUserRegistering ? onRegister() : handleLogin()} }
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
          {loading ? <CircularProgress size={24} color="inherit" /> : (newUserRegistering ? "Register" : "Log In")}
        </Button>

        {
          !newUserRegistering &&
          <Typography variant="body2" textAlign="center" color="text.secondary">
            Don't have an account?{" "}
            <Button
              variant="text"
              onClick={() => setNewUserRegistering(!newUserRegistering)}
              sx={{ textTransform: "none", color: "#007bff" }}
            >
              Register here!
            </Button>
          </Typography>
        }
      {
          newUserRegistering &&
          <Typography variant="body2" textAlign="center" color="text.secondary">
            Already have an account?{" "}
            <Button
              variant="text"
              onClick={() => setNewUserRegistering(!newUserRegistering)}
              sx={{ textTransform: "none", color: "#007bff" }}
            >
              Login Here
            </Button>
          </Typography>
        }

      </Container>
    </Box>
  );
};

export default LoginPage;