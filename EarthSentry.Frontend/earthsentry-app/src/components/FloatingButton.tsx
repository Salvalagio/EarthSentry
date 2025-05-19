import React from "react";
import { Fab } from "@mui/material";
import { Add } from "@mui/icons-material";

const FloatingButton: React.FC = ()  => {
  return (
    <Fab
      aria-label="add"
      sx={{ position: "fixed", bottom: 24, right: 24, color: "black", backgroundColor: "#1FE070", "&:hover": { backgroundColor: "#4e8a66" } }}
    >
      <Add />
    </Fab>
  );
};

export default FloatingButton;