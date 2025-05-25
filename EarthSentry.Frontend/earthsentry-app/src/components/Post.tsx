import React from "react";
import { Avatar, Box, Typography, Card, CardContent, CardMedia, Stack } from "@mui/material";
import { PostDto } from "../interfaces/PostDto";
import PostActions from "./PostActions";
import { Link } from "react-router-dom";


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
        <Link to="/postDetail" style={{ textDecoration: "none", color: "inherit" }}>
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
        </Link>
        <PostActions upvotes={upvotes} downvotes={downvotes} comments={comments} />
      </CardContent>
    </Card>
  );
};

export default Post;