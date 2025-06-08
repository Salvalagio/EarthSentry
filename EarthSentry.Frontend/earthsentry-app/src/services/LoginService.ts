import axios from "axios";
import { LoginDto } from "../interfaces/LoginDto";
import { toast } from "react-toastify";

const API_URL = "http://localhost:5253"; 

export const postUserLogin = async  (requestLogin: LoginDto): Promise<LoginDto> => {
    try {
        const response = await axios.post(`${API_URL}/Users/login`, {
            username: requestLogin.username,
            password: requestLogin.password,
        });
        return response.data;
    }
    catch (error) {
        toast.error("" + error);
        throw error;
    }
  
};