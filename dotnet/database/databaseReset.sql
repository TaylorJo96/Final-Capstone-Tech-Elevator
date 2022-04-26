--DELETE FROM invites
--DELETE FROM matches
--DELETE FROM users
--DELETE FROM leagues
--DELETE FROM courses
--DELETE FROM user_match
--DELETE FROM user_league

INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=','Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('TigerWoods','kymYFm1jEQxfrXVLz8X96BV0xXQ=','EqJbtQsHdi0=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('DustinJohnson','4IY653VnLq7Ehw2AlWqGQkLjrB8=','Sn6HyNTM2Ps=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('JustinSpieth','pPj86g6sZjbM6hIuH5An9kj+yJA=','fHW9pusmFTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('DanSchneid','Xep9nSbkHc6YDRrtWTUrp67fC0I=','60qfXu+4hRU=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('RyanHering','0F3P7O+04gwnXM9QWj0p4iCcWJM=','3TyzbEEROUM=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('TaylorDupont','4UPnM3FIxhdNY0onGLHHDX3ruwo=','Ix0X9riaEXk=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('MarioRizzardi','PnIH9SznJ5cDob71KGnIvsshfvc=','C41wgmelbko=','user');

INSERT INTO courses (course_name, address) VALUES ('Manakiki Golf Course', '35501 Eddy Rd, Willoughby, OH 44094');
INSERT INTO courses (course_name, address) VALUES ('Pebble Beach Golf Links', '1700 17 Mile Dr, Pebble Beach, CA 93953');
INSERT INTO courses (course_name, address) VALUES ('Augusta National Golf Club', '2604 Washington Rd, Augusta, GA 30904');

INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('All Users', 2, 2);
INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('The Masters', 3, 3);
INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('Tech Elevator League', 1, 1);

INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round One', 2, '2022-04-07 7:30:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Two', 2, '2022-04-08 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Three', 2, '2022-04-09 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Four', 2, '2022-04-10 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Graduation Day', 3, '2022-04-22 9:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Caddyshack Clash', 3, '1980-07-25 12:00:00 PM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Happy Gilmatch', 3, '1996-02-16 10:00:00 AM');

INSERT INTO user_league (user_id, league_id) VALUES (3, 1), (4, 1), (5, 1), (6, 1), (7, 1), (8, 1), (9, 1);
INSERT INTO user_league (user_id, league_id) VALUES (3, 2), (4, 2), (5, 2);
INSERT INTO user_league (user_id, league_id) VALUES (6, 3), (7, 3), (8, 3), (9, 3);

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (3, 1, 71, 72, '2022-04-07 10:50:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (3, 2, 74, 72, '2022-04-08 10:50:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (3, 3, 78, 72, '2022-04-09 10:50:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (3, 4, 78, 72, '2022-04-10 10:50:00 AM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (4, 1, 69, 72, '2022-04-07 1:40:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (4, 2, 73, 72, '2022-04-08 1:40:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (4, 3, 75, 72, '2022-04-09 1:40:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (4, 4, 72, 72, '2022-04-10 1:40:00 PM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (5, 1, 74, 72, '2022-04-07 12:15:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (5, 2, 76, 72, '2022-04-08 12:15:00 PM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (6, 5, 100, 70, '2022-04-22 9:00:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (6, 6, 95, 70, '1980-07-25 12:00:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (6, 7, 91, 70, '1996-02-16 10:00:00 AM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (7, 5, 120, 70, '2022-04-22 9:10:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (7, 6, 103, 70, '1980-07-25 12:10:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (7, 7, 112, 70, '1996-02-16 10:10:00 AM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (8, 5, 63, 70, '2022-04-22 9:20:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (8, 6, 65, 70, '1980-07-25 12:20:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (8, 7, 67, 70, '1996-02-16 10:20:00 AM');

INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (9, 5, 90, 70, '2022-04-22 9:30:00 AM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (9, 6, 80, 70, '1980-07-25 12:30:00 PM');
INSERT INTO user_match (user_id, match_id, score, par, tee_time) VALUES (9, 7, 70, 70, '1996-02-16 10:30:00 AM');