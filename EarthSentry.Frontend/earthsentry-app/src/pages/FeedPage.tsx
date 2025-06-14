import React, { useEffect, useRef, useState, useCallback } from "react";
import Post from "../components/Post";
import FloatingButton from "../components/FloatingButton";
import { Box, CircularProgress } from "@mui/material";
import { DarkLightModeSwitchProps } from "../types/EarthSentryAppBarProps";
import EarthSentryAppBar from "../components/EarthSentryAppBar";
import { getPosts } from "../services/PostService";
import { PostDto } from "../interfaces/PostDto";
import { useAuth } from "../contexts/AuthContext";
import PullToRefresh from "react-pull-to-refresh";
import { toast } from "react-toastify";
import FloatingButtonAdmin from "../components/FloatingButtonAdmin";

const FeedPage: React.FC<DarkLightModeSwitchProps> = ({ darkMode, onToggle }) => {
  const { userId } = useAuth();
  const [posts, setPosts] = useState<PostDto[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [loading, setLoading] = useState(false);

  const observer = useRef<IntersectionObserver | null>(null);

  const lastPostRef = useCallback((node: HTMLDivElement) => {
    if (loading) return;
    if (observer.current) observer.current.disconnect();
    observer.current = new IntersectionObserver((entries) => {
      if (entries[0].isIntersecting && hasMore) {
        setPage((prev) => prev + 1);
      }
      else if (!hasMore) {
        toast.info("There is no more posts to load :(")
      }
    });
    if (node) observer.current.observe(node);
  }, [loading, hasMore]);

  useEffect(() => {
    if (!userId) return;
    const fetchData = async () => {
      setLoading(true);
      try {
        const data = await getPosts(page, userId);
        if (page === 1) {
          setPosts(data.posts);
        } else {
          setPosts((prev) => {
            const ids = new Set(prev.map(p => p.postId));
            return [...prev, ...data.posts.filter(p => !ids.has(p.postId))];
          });
        }
        setHasMore(data.hasMore);
      } catch (err) {
        console.error("Failed to load posts:", err);
      }
      setLoading(false);
    };
    fetchData();
  }, [page, userId]);

  const handleRefresh = async () => {
    setPage(1);
  };

  return (
    <Box>
      <EarthSentryAppBar darkMode={darkMode} onToggle={onToggle} />
      <PullToRefresh onRefresh={handleRefresh} >
        <Box display="flex" flexDirection="column" gap={4} maxWidth={600} mx="auto" mt={4}>
          {posts?.map((post, index) => {
            if (index === posts.length - 1) {
              return (
                <div ref={lastPostRef} key={post.postId}>
                  <Post {...post} />
                </div>
              );
            }
            return <Post key={post.postId} {...post} />;
          })}
          {loading && <CircularProgress sx={{ alignSelf: "center" }} />}
        </Box>
      </PullToRefresh>

      <FloatingButtonAdmin />
      <FloatingButton />
    </Box>
  );
};

export default FeedPage;


