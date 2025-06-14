export interface PostDto {
  postId: number;
  description: string;
  imageUrl: string;
  userImageUrl: string;
  createdAt: Date;
  username: string;
  upVotes: number;
  downVotes: number;
  userVote?: number;
  comments: number;
  disabledActions?: boolean; // Optional property to disable actions
}
