import React from "react";
import { Fab } from "@mui/material";
import SupervisorAccountIcon from '@mui/icons-material/SupervisorAccount';
import { useNavigate } from "react-router-dom";

const FloatingButtonAdmin: React.FC = () => {
    const navigate = useNavigate();

    return (
        <Fab
            aria-label="add"
            sx={{ position: "fixed", bottom: 94, right: 24, color: "black", backgroundColor: "#2196F3", "&:hover": { backgroundColor: "#114873" } }}
            onClick={() => navigate("/AdminReport")}
        >
            <SupervisorAccountIcon />
        </Fab>
    );
};

export default FloatingButtonAdmin;