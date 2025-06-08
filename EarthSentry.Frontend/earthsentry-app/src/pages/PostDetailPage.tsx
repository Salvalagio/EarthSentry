import React from 'react';
import {
  Box,
  Avatar,
  Typography,
  IconButton,
  TextField,
  InputAdornment,
  Paper
} from '@mui/material';
import { InsertEmoticon, CameraAlt } from '@mui/icons-material';
import { ArrowLeft } from 'lucide-react';
import PostActions from '../components/PostActions';

const PostDetailPage: React.FC = () => {
  const post = {
    Id: 12345,
    user: {
      name: 'Hiroshi Martinez',
      avatar: '/path/to/avatar.jpg',
    },
    date: '1d',
    content: 'New idea: what if we create a social network that allows users to share and vote on environmental issues. That way, we can get the public opinion and influence decision making.',
    image: '/mnt/data/ed3dfd12-a04b-4f4e-91d7-dfbcaf4a1341.png',
    upvotes: 20,
    downvotes: 3,
    comments: 6,
  };

  const comments = [
    {
      user: 'Matt Henderson',
      avatar: '/path/to/avatar1.jpg',
      text: 'Great idea! We should also consider adding a feature where people can take actions on these issues',
      date: '1d'
    },
    {
      user: 'Sophia',
      avatar: '/path/to/avatar2.jpg',
      text: "Definitely, I'm in for this!",
      date: '1d'
    }
  ];


  return (
    <Box sx={{ px: 2, py: 3, maxWidth: 600, mx: 'auto' }}>
      <Box display="flex" alignItems="center" mb={2}>
        <IconButton href="/feed">
            <ArrowLeft size={20} />
        </IconButton>
        <Typography variant="h6" fontWeight="bold" ml={1}>Feed</Typography>
      </Box>

      <Box display="flex" alignItems="center" gap={2} mb={1} mt={1}>
        <Avatar src={post.user.avatar} />
        <Box>
          <Typography fontWeight={600}>{post.user.name}</Typography>
          <Typography variant="body2" color="text.secondary">{post.date}</Typography>
        </Box>
      </Box>

      <Typography mb={2}>{post.content}</Typography>
      <Box component="img" src={post.image} alt="Post image" width="100%" borderRadius={2} mb={2} />

      <PostActions upvotes={post.upvotes} downvotes={post.downvotes} comments={post.comments} disabledActions={false} postId={post.Id} />

      <Typography fontWeight="bold" mb={2} mt={2}>Comments</Typography>
      {comments.map((comment, index) => (
        <Box key={index} display="flex" gap={2} mb={2}>
          <Avatar src={comment.avatar} />
          <Box>
            <Typography fontWeight={600}>{comment.user} <Typography variant="body2" component="span" color="text.secondary">{comment.date}</Typography></Typography>
            <Typography>{comment.text}</Typography>
          </Box>
        </Box>
      ))}

      {/* Comment Input */}
      <Paper
        elevation={1}
        sx={{
          mt: 2,
          display: 'flex',
          alignItems: 'center',
          borderRadius: 4,
          px: 2,
          py: 1,
        }}
      >
        <Avatar src="/path/to/currentUser.jpg" sx={{ width: 32, height: 32, mr: 1 }} />
        <TextField
          fullWidth
          placeholder="Write a comment..."
          variant="standard"
          InputProps={{
            disableUnderline: true,
            endAdornment: (
              <InputAdornment position="end">
                <IconButton><CameraAlt /></IconButton>
                <IconButton><InsertEmoticon /></IconButton>
              </InputAdornment>
            ),
            sx: { fontSize: 16 }
          }}
        />
      </Paper>
    </Box>
  );
};

export default PostDetailPage;
