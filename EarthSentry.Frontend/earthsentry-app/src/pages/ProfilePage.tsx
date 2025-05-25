import React, { useState } from 'react';
import { Box, Typography, IconButton, Button, Avatar, Divider, TextField } from '@mui/material';
import { ArrowLeft } from 'lucide-react';
import { ProfilePageProps } from '../interfaces/ProfilePageProps';

const ProfilePage: React.FC<ProfilePageProps> = ({ onBack, onEditPhoto, onLogout, email, profileImage }) => {

    const [editingEmail, setEditingEmail] = useState(false); 
    const [editingPassword, setEditingPassword] = useState(false); 

    const toggleEmailEditing = () => {
        console.log("Toggle email editing state");
        setEditingEmail(!editingEmail);
    }

    const togglePasswordEditing = () => {
        console.log("Toggle password editing state");
        setEditingPassword(!editingPassword);
    }

    return (
        <Box sx={{ px: 2, py: 3, maxWidth: 500, mx: 'auto' }}>
            <Box display="flex" alignItems="center" mb={3}>
                <IconButton onClick={onBack}>
                    <ArrowLeft size={20} />
                </IconButton>
                <Typography variant="h6" fontWeight="bold" ml={1}>Feed</Typography>
            </Box>

            <Box display="flex" flexDirection="column" alignItems="center" mb={3}>
                <Avatar
                    src={profileImage}
                    sx={{ width: 200, height: 200, borderRadius: 4, mb: 2 }}
                    variant="rounded"
                />
                <Button
                    variant="contained"
                    onClick={onEditPhoto}
                    sx={{
                        textTransform: 'none',
                        bgcolor: '#f1f1f1',
                        color: '#000',
                        fontWeight: 'bold',
                        boxShadow: 'none',
                        '&:hover': {
                            bgcolor: '#e0e0e0',
                            boxShadow: 'none',
                        },
                    }}
                >
                    Edit Profile Photo
                </Button>
            </Box>

            <Box mb={2}>
                <Box display="flex" justifyContent="space-between" mb={1}>
                    {!editingEmail && <Typography>Email</Typography>}
                    {!editingEmail ? <Typography>{email}</Typography> :
                        <TextField
                            fullWidth
                            placeholder="Enter your new email"
                            variant="standard"
                            InputProps={{
                                sx: { fontSize: 16, display:'flex' },
                            }}
                        />}
                    
                    {!editingEmail && <Button
                        variant="contained"
                        onClick={toggleEmailEditing}
                        sx={{
                            textTransform: 'none',
                            bgcolor: '#f1f1f1',
                            color: '#000',
                            fontWeight: 'bold',
                            boxShadow: 'none',
                            '&:hover': {
                                bgcolor: '#e0e0e0',
                                boxShadow: 'none',
                            },
                        }}
                    >
                        Edit
                    </Button>}
                    {editingEmail && <Button
                        variant="contained"
                        onClick={toggleEmailEditing}
                        sx={{
                            textTransform: 'none',
                            bgcolor: '#ff2b2b',
                            color: '#ffffff',
                            fontWeight: 'bold',
                            boxShadow: 'none',
                            '&:hover': {
                                bgcolor: '#dd2a2a',
                                boxShadow: 'none',
                            },
                        }}
                    >
                        Cancel
                    </Button>}
                </Box>
                <Divider />
            </Box>

            <Box mb={4}>
                <Box display="flex" justifyContent="space-between" mb={1}>
                    {!editingPassword && <Typography>Password</Typography>}
                    {!editingPassword ? <Typography>********</Typography> :
                        <TextField
                            fullWidth
                            placeholder="Enter your new password"
                            variant="standard"
                            InputProps={{
                                sx: { fontSize: 16, display:'flex' },
                            }}
                        />}
                    {!editingPassword && <Button
                        variant="contained"
                        onClick={togglePasswordEditing}
                        sx={{
                            textTransform: 'none',
                            bgcolor: '#f1f1f1',
                            color: '#000',
                            fontWeight: 'bold',
                            boxShadow: 'none',
                            '&:hover': {
                                bgcolor: '#e0e0e0',
                                boxShadow: 'none',
                            },
                        }}
                    >
                        Edit
                    </Button>}
                    {editingPassword && <Button
                        variant="contained"
                        onClick={togglePasswordEditing}
                        sx={{
                            textTransform: 'none',
                            bgcolor: '#ff2b2b',
                            color: '#ffffff',
                            fontWeight: 'bold',
                            boxShadow: 'none',
                            '&:hover': {
                                bgcolor: '#dd2a2a',
                                boxShadow: 'none',
                            },
                        }}
                    >
                        Cancel
                    </Button>}
                </Box>
                
                {(editingPassword || editingEmail) && <Divider /> && <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={() => {
                        console.log("Changes saved");
                    }}
                    sx={{
                        borderRadius: 3,
                        py: 1.5,
                        fontWeight: 'bold',
                        textTransform: 'none',
                        fontSize: '16px'
                    }}
                >
                    Save Changes
                </Button>}
                <Divider />
            </Box>

            <Button
                variant="contained"
                color="primary"
                fullWidth
                onClick={onLogout}
                sx={{
                    borderRadius: 3,
                    py: 1.5,
                    fontWeight: 'bold',
                    textTransform: 'none',
                    fontSize: '16px'
                }}
            >
                Log Out
            </Button>
        </Box>
    );
};

export default ProfilePage;
