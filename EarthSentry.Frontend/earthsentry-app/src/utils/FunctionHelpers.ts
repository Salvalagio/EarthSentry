import { formatDistanceToNow } from 'date-fns';

export function getDistanceFromNow(createdAt?: Date): React.ReactNode {
    if (!createdAt) return '';
    const timeAgo = formatDistanceToNow(new Date(createdAt), { addSuffix: true });

    return timeAgo;
  }