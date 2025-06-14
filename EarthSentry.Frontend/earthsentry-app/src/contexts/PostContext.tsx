import { createContext, useContext, useState, ReactNode } from "react";
import { PostContextType } from "../types/PostContextType";
import { PostDto } from "../interfaces/PostDto";

const PostContext = createContext<PostContextType | undefined>(undefined);


export const PostContextProvider = ({ children }: { children: ReactNode }) => {
  const [postId, setPostId] = useState<number | null>(null);
  const [post, setPost] = useState<PostDto>();

  return (
    <PostContext.Provider value={{ postId, setPostId, post, setPost }}>
      {children}
    </PostContext.Provider>
  );
};

export const usePostContext = () => {
  const context = useContext(PostContext);
  if (!context) throw new Error("usePostContext must be used within PostContextProvider");
  return context;
};