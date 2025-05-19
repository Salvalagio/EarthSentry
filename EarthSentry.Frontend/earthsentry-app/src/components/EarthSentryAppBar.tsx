import { AppBar, Toolbar, Avatar, Typography, Box, Link } from "@mui/material";
import DarkLightModeSwitch from "./DarkLightModeSwitch";
import { DarkLightModeSwitchProps } from "../types/DarkLightModeSwitchProps";

const EarthSentryAppBar: React.FC<DarkLightModeSwitchProps> = ({ darkMode, onToggle }) => {
    return (
        <Box sx={{ position: 'static', top: 0, left: 0, right: 0, zIndex: 1100, mb: 10 }}>
            <AppBar>
                <Toolbar sx={{ position: "relative", justifyContent: "space-between" }}>
                    <Avatar src={"1231"} alt="avatar" />
                    <Link
                        href="/feed"
                        color="inherit"
                        underline="none"
                        sx={{
                            position: "absolute",
                            left: "50%",
                            transform: "translateX(-50%)",
                            textAlign: "center",
                        }}
                    >
                        <Typography variant="h6" color="inherit">
                            Feed
                        </Typography>
                    </Link>
                    <DarkLightModeSwitch darkMode={darkMode} onToggle={onToggle} />
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default EarthSentryAppBar;
