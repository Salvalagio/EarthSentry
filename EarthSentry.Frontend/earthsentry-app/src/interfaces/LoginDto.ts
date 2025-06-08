export interface LoginDto {
    userId: number | null;
    userImage: string | null;
    username: string;
    password: string;
    message: string | null;
}