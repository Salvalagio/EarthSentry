import { PostDto } from "./PostDto";

export interface FeedDto {
    posts: PostDto[];
    hasMore: boolean;
}