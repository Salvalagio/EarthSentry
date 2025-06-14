import { PostDto } from "../interfaces/PostDto";

export interface PostContextType {
  postId: number | null;
  setPostId: (id: number | null) => void;
  post?: PostDto;
  setPost: (post?: PostDto) => void;
}