import axios, { HttpStatusCode } from "axios";
import { FeedDto } from "../interfaces/FeedDto";
import { PostDto } from "../interfaces/PostDto";
import { PostCommentDto } from "../interfaces/PostCommentDto";
import { CreateCommentDto } from "../interfaces/CreateCommentDto";
import { AdminPostSummaryDto } from "../interfaces/AdminPostSummaryDto";

const API_URL = "http://localhost:5253"; 

export const getPosts = async  (page: number, userId: number): Promise<FeedDto> => {
  const response = await axios.get(`${API_URL}/Posts/feed/${userId}`, {
    params: { page },
  });
  return response.data;
};

export const getPostDetails = async  (postId: number, userId: number): Promise<PostDto> => {
  const response = await axios.get(`${API_URL}/Posts/details/${postId}`, {
    headers: {
      "userId": userId
    }
  });
  return response.data;
};


export const getPostComments = async  (postId: number, userId: number): Promise<PostCommentDto[]> => {
  const response = await axios.get(`${API_URL}/Posts/comments/${postId}`, {
    headers: {
      "userId": userId
    }
  });
  return response.data;
};


export const getAdminPosts = async (start: string, end: string): Promise<AdminPostSummaryDto[]> => {
  const response = await axios.get(`${API_URL}/Posts/admin/issues`, {
    params: {
      startDate: start,
      endDate: end
    }
  });
  return response.data;
};



export const postCreatePosts = async  (description: string, imageUrl: string, latitude:number, longitude:number, userId: number): Promise<boolean> => {
    try {
        const response = await axios.post(`${API_URL}/Posts/create/${userId}`, {
            description,
            imageUrl,
            latitude,
            longitude
        });
        return response.status === HttpStatusCode.Ok;
    }
    catch (error) {
        console.error("Error creating post:", error);
        return false;
    }
};

export const postAddPostVote = async  (userId: number, postId: number, vote:number): Promise<boolean> => {
    try {
        const response = await axios.post(`${API_URL}/Posts/vote`, {
            postId,
            userId,
            vote
          });
        return response.status === HttpStatusCode.Ok;
    }
    catch (error) {
        console.error("Error Adding post vote:", error);
        return false;
    }
};


export const postAddPostComment = async  (createPostComment: CreateCommentDto): Promise<boolean> => {
    try {
        const response = await axios.post(`${API_URL}/Posts/comments`, {
            postId: createPostComment.postId,
            userId: createPostComment.userId,
            commentDescription: createPostComment.commentDescription
          });
        return response.status === HttpStatusCode.Ok;
    }
    catch (error) {
        console.error("Error Adding post vote:", error);
        return false;
    }
};


export const deletePostVote = async  (userId: number, postId: number): Promise<boolean> => {
    try {
        const response = await axios.delete(`${API_URL}/Posts/vote`, {
            params: {
                postId                
            },
            headers: {
                "userId": userId
            }
        });
        return response.status === HttpStatusCode.Ok;
    }
    catch (error) {
        console.error("Error Adding post vote:", error);
        return false;
    }
};


export const deletePostComment = async  (userId: number, commentId: number): Promise<boolean> => {
    try {
        const response = await axios.delete(`${API_URL}/Posts/comments`, {
            params: {
                commentId                
            },
            headers: {
                "userId": userId
            }
        });
        return response.status === HttpStatusCode.Ok;
    }
    catch (error) {
        console.error("Error Adding post vote:", error);
        return false;
    }
};