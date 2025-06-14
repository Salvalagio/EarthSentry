export interface PostCommentProps extends PostCommentDto {
  onDelete?: (commentId: number) => void;
}

export interface PostCommentDto {
    commentId: number;
    userImageUrl: string;
    username: string;
    content: string;
    createAt: Date;
    isFromOwner: boolean;
};