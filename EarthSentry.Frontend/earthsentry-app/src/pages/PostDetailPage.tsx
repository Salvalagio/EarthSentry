import React, { useEffect, useState } from 'react';
import {
  Box,
  Avatar,
  Typography,
  IconButton,
  TextField,
  InputAdornment,
  Paper,
} from '@mui/material';
import { InsertEmoticon, Send } from '@mui/icons-material';
import { ArrowLeft } from 'lucide-react';
import PostActions from '../components/PostActions';
import { usePostContext } from '../contexts/PostContext';
import { useNavigate } from 'react-router-dom';
import { PostDto } from '../interfaces/PostDto';
import { getPostDetails, getPostComments, postAddPostComment } from '../services/PostService';
import { useAuth } from '../contexts/AuthContext';
import { PostCommentDto } from '../interfaces/PostCommentDto';
import { getDistanceFromNow } from '../utils/FunctionHelpers';
import { CreateCommentDto } from '../interfaces/CreateCommentDto';
import PostComment from '../components/PostComment';

const PostDetailPage: React.FC = () => {
  const navigate = useNavigate();
  const { userId, userImage } = useAuth();
  const { postId, setPost } = usePostContext();
  const [post, setPostState] = useState<PostDto>();
  const [comments, setPostComments] = useState<PostCommentDto[]>([]);
  const [commentContent, setCommentContent] = useState<string>();

  useEffect(() => {
    if (!postId || !userId) {
      return;
    }

    getPostDetails(postId, userId)
      .then((data) => {
        setPostState(data);
        setPost(data);
      })
      .catch((error) => {
        console.error("Failed to fetch post details:", error);
      });

    getPostComments(postId, userId)
      .then((data) => {
        setPostComments(data);
      }).catch((error) => {
        console.error("Failed to fetch comments details:", error);
      });
  }, [postId, userId, setPost]);


  const SendComment = async (content: string) => {
    if (!content.trim() || !userId || !postId) return;

    const newComment: CreateCommentDto = {
      userId: userId,
      postId: postId,
      commentDescription: content
    };

    if (await postAddPostComment(newComment)) {
      getPostComments(postId, userId)
        .then((data) => {
          setPostComments(data);
          if(post){
            post.comments ++;
            setPostState(post);
          }
          setCommentContent("");
        }).catch((error) => {
          console.error("Failed to fetch comments details:", error);
        });
    }
  };

  return (
    <Box sx={{ px: 2, py: 3, maxWidth: 600, mx: 'auto' }}>
      <Box display="flex" alignItems="center" mb={2}>
        <IconButton onClick={() => navigate(-1)} >
          <ArrowLeft size={20} />
        </IconButton>
        <Typography variant="h6" fontWeight="bold" ml={1}>Feed</Typography>
      </Box>

      <Box display="flex" alignItems="center" gap={2} mb={1} mt={1}>
        <Avatar src={post?.userImageUrl} />
        <Box>
          <Typography fontWeight={600}>{post?.username}</Typography>
          <Typography variant="body2" color="text.secondary">{getDistanceFromNow(post?.createdAt)}</Typography>
        </Box>
      </Box>

      <Typography mb={2}>{post?.description}</Typography>
      <Box component="img" src={post?.imageUrl} alt="Post image" width="100%" borderRadius={2} mb={2} />

      {post && <PostActions {...post} />}

      <Typography fontWeight="bold" mb={2} mt={2}>Comments</Typography>
      {comments.map((comment, index) => (
        <PostComment
          key={comment.commentId ?? index}
          {...comment}
          onDelete={(id) => {
            setPostComments(prev => prev.filter(c => c.commentId !== id));
            if (post) {
              const updatedPost = { ...post, comments: post.comments - 1 };
              setPostState(updatedPost);
            }
          }}
        />
      ))}

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
        <Avatar src={userImage ?? undefined} sx={{ width: 32, height: 32, mr: 1 }} />
        <TextField
          fullWidth
          placeholder="Write a comment..."
          variant="standard"
          value={commentContent}
          onChange={(e) => setCommentContent(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === 'Enter' && commentContent) {
              e.preventDefault();
              SendComment(commentContent);
            }
          }}
          InputProps={{
            disableUnderline: true,
            endAdornment: (
              <InputAdornment position="end">
                <IconButton><InsertEmoticon /></IconButton>
                <IconButton onClick={() => SendComment(commentContent ?? "")}>
                  <Send />
                </IconButton>
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
