import React, { createContext, useContext, useState, ReactNode } from "react";
import { AuthContextType } from "../types/AuthContextType";

const AuthContext = createContext<AuthContextType | undefined>(undefined);


export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [userId, setUserId] = useState<number | null>(null);
  const [userImage, setUserImage] = useState<string | null>(null);
  const [password, setPassword] = useState<string | null>(null);
  const [,actionLogoutUser] = useState(() => {
    setUserId(null);
    setUserImage(null);
    setPassword(null);
  });

  return (
    <AuthContext.Provider value={{ userId, setUserId, userImage, setUserImage, password, setPassword, actionLogoutUser}}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within AuthProvider");
  return context;
};