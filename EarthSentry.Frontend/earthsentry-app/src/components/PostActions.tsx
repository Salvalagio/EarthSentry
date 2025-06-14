import React, { useEffect } from "react";
import { Box, IconButton, Stack } from "@mui/material";
import { PostActionsDto } from "../interfaces/PostActionsDto";
import { ArrowDown, ArrowUp, MessageCircleMore } from 'lucide-react';
import { useAuth } from "../contexts/AuthContext";
import { postAddPostVote, deletePostVote } from "../services/PostService";
import { useLocation, useNavigate } from "react-router-dom";
import { usePostContext } from "../contexts/PostContext";


const PostActions: React.FC<PostActionsDto> = (postActions:PostActionsDto) => {
    const { userId } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    const { setPostId } = usePostContext();
    const [userVoteState, setUserVoteState] = React.useState(postActions.userVote);
    const [upVotesState, setUpVotesState] = React.useState((postActions.upVotes ?? 0) + (userVoteState ?? 0) === 1 ? 1 : 0);
    const [downVotesState, setDownVotesState] = React.useState( Math.abs(postActions.downVotes ?? 0) + (userVoteState ?? 0) === -1 ? 1 : 0); 


    useEffect(() => {
        setUserVoteState(postActions.userVote);
    }, [postActions.userVote]);

    useEffect(() => {
        setUpVotesState(postActions.upVotes ?? 0);
    }, [postActions.upVotes]);

    useEffect(() => {
        setDownVotesState(Math.abs(postActions.downVotes ?? 0));
    }, [postActions.downVotes]);



    const handlePostvote = async (vote: number, currentUserVote: number) => {
        if (postActions.disabledActions) 
            return;

        if(userId){
            if(currentUserVote === vote){
                // User is trying to remove their vote
                setUserVoteState(0);
                setUpVotesState(prev => vote === 1 ? prev - 1 : prev);
                setDownVotesState(prev => vote === -1 ? prev - 1 : prev);
                await deletePostVote(userId, postActions.postId ?? 0);
            }
            else if(currentUserVote === 0){
                // User is voting for the first time
                await postAddPostVote(userId, postActions.postId ?? 0, vote);
                setUserVoteState(vote);

                if(vote === 1)
                    setUpVotesState(prev => prev + 1);
                else if(vote === -1)
                    setDownVotesState(prev => prev + 1);
            }
            else if(currentUserVote !== vote){
                // User is changing their vote
                await deletePostVote(userId, postActions.postId ?? 0);
                await postAddPostVote(userId, postActions.postId ?? 0, vote);
                setUserVoteState(vote);

                if(vote === 1){
                    setUpVotesState(prev => prev + 1);
                    setDownVotesState(prev => prev - 1);
                }
                else if(vote === -1){
                    setUpVotesState(prev => prev - 1);
                    setDownVotesState(prev => prev + 1);
                }
            }
        }
    }

    const handleComment = () => {
        if (postActions.disabledActions) return;
        if (location.pathname === "/postDetail") return;
        
        setPostId(postActions.postId ?? 0);
        navigate("/postDetail");
    }

    return (
        <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ width: "100%", flexWrap: "wrap", mt: 2 }}>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "flex-start", gap: 1 }}>
                <IconButton size="small" disabled={postActions.disabledActions} onClick={() => handlePostvote(1 , userVoteState ?? 0)} sx={{ color: userVoteState === 1 ? "green" : "inherit"}}>
                    <ArrowUp /> {upVotesState ?? 0}
                </IconButton>
            </Box>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "center", gap: 1 }}>
                <IconButton size="small" disabled={postActions.disabledActions} onClick={() => handlePostvote(-1, userVoteState ?? 0)}sx={{ color: userVoteState === -1 ? "green" : "inherit"}}>
                    <ArrowDown /> {downVotesState ?? 0}
                </IconButton>
            </Box>
            <Box sx={{ flex: 1, display: "flex", justifyContent: "flex-end", gap: 1 }}>
                <IconButton size="small" disabled={postActions.disabledActions} onClick={handleComment}>
                    <MessageCircleMore /> {postActions.comments ?? 0}
                </IconButton>
            </Box>
        </Stack>
    );
}

export default PostActions;