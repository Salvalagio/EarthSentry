import React from "react";
import { Fab } from "@mui/material";
import { Add } from "@mui/icons-material";

const FloatingButton: React.FC = () => {
  return (
    <Fab
      color="primary"
      aria-label="add"
      sx={{ position: "fixed", bottom: 24, right: 24 }}
    >
      <Add />
    </Fab>
  );
};

export default FloatingButton;