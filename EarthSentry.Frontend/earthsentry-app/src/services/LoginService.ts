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

export const postUserRegister = async  (requestLogin: LoginDto): Promise<LoginDto> => {
    try {
        const response = await axios.post(`${API_URL}/Users/register`, {
            username: requestLogin.username,
            password: requestLogin.password,
            email: requestLogin.email,
        });

        return response.data;
    }
    catch (error) {
        toast.error("" + error);
        throw error;
    }
};

export const putUserUpdate = async  (requestLogin: LoginDto): Promise<boolean> => {
    try {
        const response = await axios.put(`${API_URL}/Users/update`, {
            email: requestLogin.email,
            newPassword: requestLogin.password,
            imageUrl: requestLogin.imageUrl,
            userId: requestLogin.userId,
        });
        return response.status === 200;
    }
    catch (error) {
        toast.error("" + error);
        throw error;
    }
};

export const getUserById = async  (userId: number, password: string): Promise<LoginDto> => {
    try {
        const response = await axios.get(`${API_URL}/Users/${userId}`,
            {
                headers: {
                    'password': password,
                },
            }
         );
        return response.data;
    }
    catch (error) {
        toast.error("" + error);
        throw error;
    }
};