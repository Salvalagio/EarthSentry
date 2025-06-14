export interface AuthContextType {
  userId: number | null;
  setUserId: (id: number | null) => void;  
  userImage: string | null;
  setUserImage: (image: string | null) => void;
  password: string | null;
  setPassword: (password: string | null) => void;
  actionLogoutUser: () => void;
}
