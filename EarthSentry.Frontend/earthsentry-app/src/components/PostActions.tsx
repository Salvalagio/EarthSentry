import React from "react";
import { Box, IconButton, Stack } from "@mui/material";
import { PostActionsDto } from "../interfaces/PostActionsDto";
import { ArrowDown, ArrowUp, MessageCircleMore } from 'lucide-react';

const PostActions: React.FC<PostActionsDto> = ({ upvotes, downvotes, comments, disabledActions = false }) => {
    return (
        <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ width: "100%", flexWrap: "wrap", mt: 2 }}>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "flex-start", gap: 1 }}>
                <IconButton size="small" disabled={disabledActions}>
                    <ArrowUp /> {upvotes}
                </IconButton>
            </Box>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "center", gap: 1 }}>
                <IconButton size="small" disabled={disabledActions}>
                    <ArrowDown /> {downvotes}
                </IconButton>
            </Box>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "flex-end", gap: 1 }}>
                {/*TODO: adjust this button to call "PostDetail" page and trigger the consult of comments to show better for our user.*/}
                <IconButton size="small" disabled={disabledActions}>
                    <MessageCircleMore /> {comments}
                </IconButton>
            </Box>
        </Stack>
    );
}

export default PostActions;