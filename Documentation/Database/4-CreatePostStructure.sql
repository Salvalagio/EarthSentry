CREATE TABLE earth.tb_posts (
    postid SERIAL PRIMARY KEY,
    userid INT NOT NULL REFERENCES earth.tb_users(userid) ON DELETE CASCADE,
    description TEXT NOT NULL,
    imageurl TEXT NOT NULL,
    latitude DECIMAL(9,6),
    longitude DECIMAL(9,6),
    createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedat TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE earth.tb_postvotes (
    postvoteid SERIAL PRIMARY KEY,
    postid INT NOT NULL REFERENCES earth.tb_posts(postid) ON DELETE CASCADE,
    userid INT NOT NULL REFERENCES earth.tb_users(userid) ON DELETE CASCADE,
    vote SMALLINT NOT NULL CHECK (vote IN (1, -1)), -- 1 = upvote, -1 = downvote
    createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT uq_post_user UNIQUE (postid, userid) -- user can only vote once per post
);

CREATE TABLE earth.tb_comments (
    commentid SERIAL PRIMARY KEY,
    postid INTEGER NOT NULL REFERENCES earth.tb_posts(postid) ON DELETE CASCADE,
    userid INTEGER NOT NULL REFERENCES earth.tb_users(userid) ON DELETE CASCADE,
    content TEXT NOT NULL,
    createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION earth.fn_update_post_updatedat()
RETURNS TRIGGER AS $$
BEGIN
   NEW.updatedat = CURRENT_TIMESTAMP;
   RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_post_updatedat
BEFORE UPDATE ON earth.tb_posts
FOR EACH ROW
EXECUTE FUNCTION earth.fn_update_post_updatedat();
