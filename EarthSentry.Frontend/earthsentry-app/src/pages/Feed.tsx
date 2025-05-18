import React from "react";
import Post from "../components/Post";
import FloatingButton from "../components/FloatingButton";
import { Box } from "@mui/material";

const posts = [
  {
    id: 1,
    username: "Eco-friendly toothpaste",
    timestamp: "1d",
    content:
      "I've been using Bite toothpaste for a year now and it's my favorite. It's zero waste, plastic free and vegan. Highly recommend.",
    imageUrl: "https://via.placeholder.com/600x300.png?text=Toothpaste",
    avatarUrl: "https://via.placeholder.com/50",
    upvotes: 20,
    downvotes: 3,
    comments: 6,
  },
  {
    id: 2,
    username: "Save the bees",
    timestamp: "2d",
    content:
      "The bee population is declining rapidly due to climate change, pesticides and diseases. What we can do to help them?",
    imageUrl: "https://via.placeholder.com/600x300.png?text=Bees",
    avatarUrl: "https://via.placeholder.com/50",
    upvotes: 20,
    downvotes: 3,
    comments: 6,
  },
];

const FeedPage: React.FC = () => {
  return (
    <Box>
      <Box display="flex" flexDirection="column" gap={4} maxWidth={600} mx="auto">
        {posts.map((post) => (
          <Post key={post.id} {...post} />
        ))}
      </Box>
      <FloatingButton />
    </Box>
  );
};

export default FeedPage;