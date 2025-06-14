export interface LoginDto {
    userId: number | null;
    imageUrl: string | null;
    username: string | null;
    password: string;
    email: string | null;
    message: string | null;
}