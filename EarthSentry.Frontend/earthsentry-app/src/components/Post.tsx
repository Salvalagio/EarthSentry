import React from "react";
import { Avatar, Box, Typography, Card, CardContent, CardMedia, Stack } from "@mui/material";
import { PostDto } from "../interfaces/PostDto";
import PostActions from "./PostActions";


const Post: React.FC<PostDto> = ({
  postId,
  description,
  imageUrl,
  userImageUrl,
  username,
  upvotes,
  downvotes,
  comments,
  disabledActions = false,
}) => {
  return (
    <Card>
      <CardContent>
          <Box display="flex" alignItems="center" mb={2}>
            <Avatar src={userImageUrl} alt="avatar" />
            <Box ml={2}>
              <Typography fontWeight="bold">{username}</Typography>
              <Typography variant="caption" color="text.secondary">
                {"1d"} {/* TBD timestamp that post was created */}
              </Typography>
            </Box>
          </Box>
          <Typography variant="body1" mb={2}>
            {description}
          </Typography>
          <CardMedia
            component="img"
            image={imageUrl}
            alt="Post visual"
            sx={{ borderRadius: 2, mb: 2, maxHeight: 300, objectFit: "cover" }}
          />
        <PostActions upvotes={upvotes} downvotes={downvotes} comments={comments} disabledActions={disabledActions} postId={postId} />
      </CardContent>
    </Card>
  );
};

export default Post;