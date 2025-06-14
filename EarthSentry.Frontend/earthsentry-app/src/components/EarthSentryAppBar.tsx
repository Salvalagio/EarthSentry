import { AppBar, Toolbar, Avatar, Typography, Box, Button } from "@mui/material";
import DarkLightModeSwitch from "./DarkLightModeSwitch";
import { EarthSentryAppBarProps } from "../types/EarthSentryAppBarProps";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";

const EarthSentryAppBar: React.FC<EarthSentryAppBarProps> = ({ darkMode, onToggle }) => {
    const { userImage } = useAuth();
    const navigate = useNavigate();

    const onAvatarClick = () => {
        navigate("/profilePage");
    }
    return (
        <Box sx={{ position: 'static', top: 0, left: 0, right: 0, zIndex: 1100, mb: 10 }}>
            <AppBar>
                <Toolbar sx={{ position: "relative", justifyContent: "space-between" }}>
                    <Button color="inherit" onClick={onAvatarClick} sx={{
                        position: "relative",
                        borderRadius: 50,
                    }} >
                        <Avatar src={userImage ?? undefined} alt="avatar" />
                    </Button>
                    <Typography variant="h6" color="inherit">
                        Feed
                    </Typography>
                    <DarkLightModeSwitch darkMode={darkMode} onToggle={onToggle} />
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default EarthSentryAppBar;
