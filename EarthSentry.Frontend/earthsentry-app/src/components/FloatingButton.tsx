import React, { useState } from "react";
import { Fab } from "@mui/material";
import { Add } from "@mui/icons-material";
import StartPostDialog from "../components/StartPostDialog";

const FloatingButton: React.FC = () => {
  const [open, setOpen] = useState(false);

  if (!open) {
    return (
      <Fab
        aria-label="add"
        sx={{ position: "fixed", bottom: 24, right: 24, color: "black", backgroundColor: "#1FE070", "&:hover": { backgroundColor: "#4e8a66" } }}
        onClick={() => setOpen(open => !open)}
      >
        <Add />
      </Fab>
    );
  }
  else {
    return (
      <StartPostDialog open={open} onClose={() => setOpen(false)}/>
    );
  }
};

export default FloatingButton;