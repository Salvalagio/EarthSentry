import React, { useEffect, useState } from 'react';
import { Box, Typography, IconButton, Button, Avatar, Divider, TextField, CircularProgress } from '@mui/material';
import { ArrowLeft } from 'lucide-react';
import { useAuth } from '../contexts/AuthContext';
import { getUserById, putUserUpdate } from '../services/LoginService';
import { LoginDto } from '../interfaces/LoginDto';
import { useNavigate } from 'react-router-dom';
import { uploadImageToCloudinary } from '../services/CloudinaryService';
import { toast } from 'react-toastify';

const ProfilePage: React.FC = () => {
    const navigate = useNavigate();
    const { userId, password, actionLogoutUser, setUserImage } = useAuth();
    const [user, setUser] = useState<LoginDto>();
    const [loading, setLoading] = useState(false);
    const [editingEmail, setEditingEmail] = useState(false);
    const [editingPassword, setEditingPassword] = useState(false);
    const [editingProfilePicture, setEditingProfilePicture] = useState(false);

    useEffect(() => {
        const fetchUser = async () => {
            if (!userId || !password) {
                console.error("User ID is not available");
                return;
            }
            var loginDto = await getUserById(userId, password)
            setUser(loginDto);
        };
        fetchUser();

    }, [userId, password]);

    const onBack = () => {
        navigate('/feed');
    };

    const onEditPhoto = () => {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.onchange = async (event: any) => {
            const file = event.target.files && event.target.files[0];
            if (file) {
                setLoading(true);
                try {
                    var ret = await uploadImageToCloudinary(file);
                    if (ret) {
                        setUser((prevUser) => prevUser ? {
                            ...prevUser,
                            imageUrl: ret,
                        } : prevUser);
                        if (!editingProfilePicture)
                            setEditingProfilePicture(!editingProfilePicture);
                    }
                    else if (editingProfilePicture) {
                        setEditingProfilePicture(!editingProfilePicture);
                    }
                }
                catch (error) {
                    console.error('Error uploading image:', error);
                    toast.error('Failed to upload image. Please try again.');
                }
                finally {
                    setLoading(false);
                }
            }

        };
        input.click();
    };

    const onSaveChanges = async () => {
        var ret = await putUserUpdate({
            userId: user?.userId || 0,
            email: editingEmail ? user?.email || '' : '',
            password: editingPassword ? user?.password || '' : '',
            imageUrl: user?.imageUrl || '',
            username: null,
            message: null
        })


        if (ret) {
            toast.success('Changes saved successfully!');
            setUserImage(user?.imageUrl || '');
            setEditingEmail(false);
            setEditingPassword(false);
            setEditingProfilePicture(false);
        }
        else
            toast.error('Changes could not be saved, try again!');
    }

    const onLogout = () => {
        actionLogoutUser();
        navigate('/');
    };

    return (
        <Box sx={{ px: 2, py: 3, maxWidth: 500, mx: 'auto' }}>
            <Box display="flex" alignItems="center" mb={3}>
                <IconButton onClick={onBack}>
                    <ArrowLeft size={20} />
                </IconButton>
                <Typography variant="h6" fontWeight="bold" ml={1}>Feed</Typography>
            </Box>

            <Box display="flex" flexDirection="column" alignItems="center" mb={3}>
                {
                    loading ?
                        <CircularProgress size={24} color="inherit" sx={{mb:20}} /> :
                (
                    <>
                        <Avatar
                            src={user?.imageUrl || ''}
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
                    </>
                )
                        
                }
                
            </Box>

            <Box mb={2}>
                <Box display="flex" justifyContent="space-between" mb={1}>
                    {!editingEmail && <Typography>Email</Typography>}
                    {!editingEmail ? <Typography>{user?.email ?? ''}</Typography> :
                        <TextField
                            fullWidth
                            placeholder="Enter your new email"
                            variant="standard"
                            InputProps={{
                                sx: { fontSize: 16, display: 'flex' },
                            }}
                        />}

                    {!editingEmail && <Button
                        variant="contained"
                        onClick={() => setEditingEmail(!editingEmail)}
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
                        onClick={() => setEditingEmail(!editingEmail)}
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
                                sx: { fontSize: 16, display: 'flex' },
                            }}
                        />}
                    {!editingPassword && <Button
                        variant="contained"
                        onClick={() => setEditingPassword(!editingPassword)}
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
                        onClick={() => setEditingPassword(!editingPassword)}
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

                {(editingPassword || editingEmail || editingProfilePicture) && <Divider /> && <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    onClick={onSaveChanges}
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
