import { Button, Box } from "@mui/material";
import { SunIcon, MoonIcon } from "lucide-react";
import React from "react";
import { DarkLightModeSwitchProps } from "../types/EarthSentryAppBarProps";



const DarkLightModeSwitch: React.FC<DarkLightModeSwitchProps> = ({ darkMode, onToggle }) => {
    return (
        <Box sx={{ zIndex: 1 }}>
            <Button color="inherit" onClick={onToggle} sx={{
                position: "relative",
                height: 40,
                borderRadius: 50,
            }} >
                <Box
                    sx={{
                        position: "absolute",
                        top: 0,
                        left: 0,
                        width: "100%",
                        height: "100%",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        transition: "opacity 1s ease, transform 1s ease",
                        opacity: darkMode ? 1 : 0,
                        transform: darkMode ? "scale(1)" : "scale(0.8)",
                    }}
                >
                    <SunIcon size={24} />
                </Box>
                <Box
                    sx={{
                        position: "absolute",
                        top: 0,
                        left: 0,
                        width: "100%",
                        height: "100%",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        transition: "opacity 1s ease, transform 1s ease",
                        opacity: darkMode ? 0 : 1,
                        transform: darkMode ? "scale(0.8)" : "scale(1)",
                    }}
                >
                    <MoonIcon size={24} />
                </Box>
            </Button>
        </Box>
    )

};


export default DarkLightModeSwitch;
