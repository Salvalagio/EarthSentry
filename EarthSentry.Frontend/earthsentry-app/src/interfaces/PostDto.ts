export interface PostDto {
  postId: number;
  description: string;
  imageUrl: string;
  userImageUrl: string;
  username: string;
  upvotes: number;
  downvotes: number;
  comments: number;
  disabledActions?: boolean; // Optional property to disable actions
}
