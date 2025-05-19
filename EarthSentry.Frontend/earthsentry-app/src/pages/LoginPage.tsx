import React from "react";
import {
  Box,
  Button,
  Container,
  TextField,
  Typography,
} from "@mui/material";

const LoginPage: React.FC = () => {
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
          label="Email"
          variant="outlined"
          fullWidth
          InputProps={{ sx: { borderRadius: 2 } }}
        />

        <TextField
          label="Password"
          variant="outlined"
          type="password"
          fullWidth
          InputProps={{ sx: { borderRadius: 2 } }}
        />

        <Button
          variant="contained"
          fullWidth
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
          Log In
        </Button>
      </Container>
    </Box>
  );
};

export default LoginPage;