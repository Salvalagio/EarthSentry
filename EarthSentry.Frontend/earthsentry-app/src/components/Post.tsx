import React from "react";
import { Avatar, Box, Typography, Card, CardContent, CardMedia, Stack } from "@mui/material";
import { ArrowUpward, ArrowDownward, ChatBubbleOutline } from "@mui/icons-material";
import { PostDto } from "../interfaces/PostDto";


const Post: React.FC<PostDto> = ({
  username,
  timestamp,
  content,
  imageUrl,
  avatarUrl,
  upvotes,
  downvotes,
  comments,
}) => {
  return (
    <Card>
      <CardContent>
        <Box display="flex" alignItems="center" mb={2}>
          <Avatar src={avatarUrl} alt="avatar" />
          <Box ml={2}>
            <Typography fontWeight="bold">{username}</Typography>
            <Typography variant="caption" color="text.secondary">
              {timestamp}
            </Typography>
          </Box>
        </Box>
        <Typography variant="body1" mb={2}>
          {content}
        </Typography>
        <CardMedia
          component="img"
          image={imageUrl}
          alt="Post visual"
          sx={{ borderRadius: 2, mb: 2, maxHeight: 300, objectFit: "cover" }}
        />
        <Stack direction="row" spacing={4} justifyContent="center">
          <Box display="flex" alignItems="center" gap={1}>
            <ArrowUpward /> {upvotes}
          </Box>
          <Box display="flex" alignItems="center" gap={1}>
            <ArrowDownward /> {downvotes}
          </Box>
          <Box display="flex" alignItems="center" gap={1}>
            <ChatBubbleOutline /> {comments}
          </Box>
        </Stack>
      </CardContent>
    </Card>
  );
};

export default Post;