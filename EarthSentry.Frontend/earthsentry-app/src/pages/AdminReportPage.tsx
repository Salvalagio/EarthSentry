import React, { useEffect, useState } from "react";
import {
    Box,
    Typography,
    TextField,
    Card,
    CardMedia,
    CardContent,
    Grid
} from "@mui/material";
import { AdminPostSummaryDto } from "../interfaces/AdminPostSummaryDto";
import { ArrowBack } from "@mui/icons-material";
import { IconButton } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { getAdminPosts } from "../services/PostService";
import { toast } from "react-toastify";
import { usePostContext } from "../contexts/PostContext";

const AdminPage: React.FC = () => {
    const navigate = useNavigate();    
    const { setPostId } = usePostContext();
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [posts, setPosts] = useState<AdminPostSummaryDto[]>([]);

    useEffect(() => {
        const savedStartDate = localStorage.getItem("adminStartDate");
        const savedEndDate = localStorage.getItem("adminEndDate");

        if (savedStartDate) {
            setStartDate(savedStartDate);
        }
        if (savedEndDate) {
            setEndDate(savedEndDate);
        }
    }, []);

    useEffect(() => {
        if (!startDate || !endDate) return;

        if (startDate > endDate) {
            toast.error("Start date cannot be after end date.");
            return;
        }

        getAdminPosts(startDate, endDate)
            .then((data) => {
                if (data.length === 0) {
                    toast.info("No posts found for the selected date range.");
                }
                setPosts(data);
                localStorage.setItem("adminStartDate", startDate);
                localStorage.setItem("adminEndDate", endDate);
            })
            .catch((error) => {
                console.error("Failed to fetch admin posts:", error)
                toast.error("Failed to fetch posts. Please try again later.")
            });
    }, [startDate, endDate]);

    

    function OnGoToPost(postId:number): void {
        setPostId(postId);
        navigate("/postDetail");
    }

    return (
        <Box sx={{ p: 2 }}>
            <Box display="flex" alignItems="center" mb={2}>
                <IconButton onClick={() => navigate("/feed")}>
                    <ArrowBack />
                </IconButton>
                <Typography variant="h6" fontWeight="bold" ml={1}>
                    Admin page
                </Typography>
            </Box>

            <Typography variant="subtitle1" mb={1}>Date range</Typography>
            <Box display="flex" gap={2} mb={3}>
                <TextField
                    type="date"
                    value={startDate}
                    onChange={(e) => setStartDate(e.target.value)}
                    sx={{ flex: 1 }}
                />
                <TextField
                    type="date"
                    value={endDate}
                    onChange={(e) => setEndDate(e.target.value)}
                    sx={{ flex: 1 }}
                />
            </Box>

            <Typography variant="h6" fontWeight="bold" mb={2}>Issues</Typography>
            <Grid container spacing={2}>
                {posts.map((post) => (
                    <Grid key={post.postId}>
                        <Card onClick={() => OnGoToPost(post.postId)} sx={{ cursor: 'pointer' }}>
                            <CardMedia
                                component="img"
                                height="140"
                                image={post.imageUrl}
                                alt={post.title}
                            />
                            <CardContent>
                                <Typography variant="subtitle1" fontWeight={600}>
                                    {post.title}
                                </Typography>
                                <Typography variant="body2" color="text.secondary">
                                    {post.category} - {post.voteCount} votes
                                </Typography>
                            </CardContent>
                        </Card>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
};

export default AdminPage;
