export interface PostActionsDto {
  postId: number;
  upvotes: number;
  downvotes: number;
  comments: number;
  disabledActions?: boolean; 
}