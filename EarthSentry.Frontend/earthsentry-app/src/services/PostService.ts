import axios, { HttpStatusCode } from "axios";
import { FeedDto } from "../interfaces/FeedDto";

const API_URL = "http://localhost:5253"; 

export const getPosts = async  (page: number, userId: number): Promise<FeedDto> => {
  const response = await axios.get(`${API_URL}/Posts/feed/${userId}`, {
    params: { page },
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