import React from "react";
import { Avatar, Box, Typography, Card, CardContent, CardMedia } from "@mui/material";
import { PostDto } from "../interfaces/PostDto";
import PostActions from "./PostActions";
import { getDistanceFromNow } from "../utils/FunctionHelpers";

const Post: React.FC<PostDto> = (post:PostDto) => {
  return (
    <Card>
      <CardContent>
          <Box display="flex" alignItems="center" mb={2}>
            <Avatar src={post.userImageUrl} alt="avatar" />
            <Box ml={2}>
              <Typography fontWeight="bold">{post.username}</Typography>
              <Typography variant="caption" color="text.secondary">
                {getDistanceFromNow(post.createdAt ?? undefined)} {/* TBD timestamp that post was created */}
              </Typography>
            </Box>
          </Box>
          <Typography variant="body1" mb={2}>
            {post.description}
          </Typography>
          <CardMedia
            component="img"
            image={post.imageUrl}
            alt="Post visual"
            sx={{ borderRadius: 2, mb: 2, maxHeight: 300, objectFit: "cover" }}
          />
        <PostActions {...post} />
      </CardContent>
    </Card>
  );
};

export default Post;