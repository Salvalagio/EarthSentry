import { MoreVert } from "@mui/icons-material";
import { Avatar, Typography, IconButton, MenuItem, Box, Menu } from "@mui/material";
import { useState } from "react";
import { PostCommentProps } from "../interfaces/PostCommentDto";
import { getDistanceFromNow } from "../utils/FunctionHelpers";
import { useAuth } from "../contexts/AuthContext";
import { deletePostComment } from '../services/PostService';

const PostComment: React.FC<PostCommentProps> = ({
    commentId,
    userImageUrl,
    username,
    content,
    createAt,
    isFromOwner,
    onDelete
}) => {
    const { userId } = useAuth();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };
    const handleDeleteComment = async () => {
        if (!commentId || !userId) return;
        const success = await deletePostComment(userId, commentId);
        if (success) {
            onDelete?.(commentId);
        }
        handleMenuClose();
    };
    return (

        <Box display="flex" gap={2} mb={2} sx={{ position: 'relative' }}>
            <Avatar src={userImageUrl} />
            <Box flex={1}>
                {isFromOwner && (
                    <>
                        <IconButton
                            size="small"
                            onClick={handleMenuOpen}
                            sx={{ position: 'absolute', top: 0, right: 0 }}
                        >
                            <MoreVert fontSize="small" />
                        </IconButton>
                        <Menu
                            anchorEl={anchorEl}
                            open={open}
                            onClose={handleMenuClose}
                            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
                        >
                            <MenuItem onClick={handleDeleteComment}>Delete</MenuItem>
                        </Menu>
                    </>
                )}

                <Typography fontWeight={600}>
                    {username}{' '}
                    <Typography variant="body2" component="span" color="text.secondary">
                        {getDistanceFromNow(createAt)}
                    </Typography>
                </Typography>

                <Typography>{content}</Typography>
            </Box>
        </Box>
    );
}

export default PostComment;