import { AppBar, Toolbar, Avatar, Typography, Box, Link } from "@mui/material";
import DarkLightModeSwitch from "./DarkLightModeSwitch";
import { EarthSentryAppBarProps } from "../types/EarthSentryAppBarProps";
import { useAuth } from "../contexts/AuthContext";

const EarthSentryAppBar: React.FC<EarthSentryAppBarProps> = ({ darkMode, onToggle }) => {
    const { userImage } = useAuth();

    return (
        <Box sx={{ position: 'static', top: 0, left: 0, right: 0, zIndex: 1100, mb: 10 }}>
            <AppBar>
                <Toolbar sx={{ position: "relative", justifyContent: "space-between" }}>
                    <Avatar src={userImage ?? undefined} alt="avatar" />
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
