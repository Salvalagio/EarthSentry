import React, { useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    IconButton,
    TextField,
    Typography,
    Button,
    Box,
    CircularProgress,
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import ImageIcon from '@mui/icons-material/Image';
import ClearIcon from '@mui/icons-material/Clear';
import { StartPostDialogProps } from '../interfaces/StartPostDialogProps';
import { useAuth } from '../contexts/AuthContext';
import { postCreatePosts } from '../services/PostService';
import { uploadImageToCloudinary } from '../services/CloudinaryService';
import { toast } from "react-toastify";
export const StartPostDialog: React.FC<StartPostDialogProps> = ({open, onClose}) => {
    const { userId } = useAuth();
    const [description, setDescription] = useState('');
    const [photo, setPhoto] = useState<File | null>(null);
    const [loading, setLoading] = useState(false);
    
    async function handleSubmit() {
        try {
            setLoading(true);

            //future features: location.
            var latitude = 0;
            var longitude = 0;

            var cloudinaryUri = '';

            if (photo)
                cloudinaryUri = await uploadImageToCloudinary(photo);

            var ret = await postCreatePosts(description, cloudinaryUri, latitude, longitude, userId!);

            if (ret)
                handleClose();
            else
                toast.error('Failed to create post. Please try again!');
        }
        catch (error) {
            console.error('Error creating post:', error);
            toast.error('Failed to create post. Please try again!');
        }
        finally {
            setLoading(false);
        }
       
    };

    const handleClose = () => {
        setDescription('');
        setPhoto(null);
        onClose();
    }

    return (
        loading ? 
            <CircularProgress sx={{ alignSelf: "center" }} /> 
        :
            <Dialog open={open} fullWidth maxWidth="sm" >
                <DialogTitle sx={{ textAlign: 'center' }}>
                    <Typography fontWeight="bold">Start post</Typography>
                    <IconButton onClick={handleClose} sx={{ position: 'absolute', left: 8, top: 8 }}>
                        <CloseIcon />
                    </IconButton>
                </DialogTitle>

                <DialogContent>
                    <TextField
                        variant="filled"
                        fullWidth
                        multiline
                        placeholder="Add a description"
                        value={description}
                        onChange={e => setDescription(e.target.value)}
                        sx={{ mb: 2 }}
                    />

                    {
                    photo 
                    ? 
                    <Box display="flex" alignItems="center" mb={2}>
                        <IconButton component="label" onClick={() => setPhoto(null)}>
                            <ImageIcon />
                            <Typography ml={1} variant="body2">1 Photo added</Typography>
                            <ClearIcon />
                        </IconButton>
                    </Box>
                    :
                    <Box display="flex" alignItems="center" mb={2}>
                        <IconButton component="label">
                            <ImageIcon />
                            <input
                                hidden
                                accept="image/*"
                                type="file"
                                onChange={e => setPhoto(e.target.files?.[0] || null)}
                            />
                            <Typography ml={1} variant="body2">Add a photo</Typography>
                            <Typography ml={1} variant="caption" color="text.secondary">(Max size 5MB)</Typography>
                        </IconButton>
                    </Box>
                    }

                    <Button variant="contained" fullWidth onClick={handleSubmit}>
                        Submit
                    </Button>
                </DialogContent>
            </Dialog>
    );
};

export default StartPostDialog;