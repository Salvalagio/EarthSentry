export interface ProfilePageProps {
  onBack: () => void;
  onEditPhoto: () => void;
  onLogout: () => void;
  email: string;
  profileImage: string;
}