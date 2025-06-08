export interface AuthContextType {
  userId: number | null;
  setUserId: (id: number | null) => void;  
  userImage: string | null;
  setUserImage: (image: string | null) => void;
}
