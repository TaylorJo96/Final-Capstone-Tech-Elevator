USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username nvarchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT [PK_user] PRIMARY KEY (user_id)
)
CREATE TABLE courses(
	course_id int IDENTITY (1,1) NOT NULL,
	course_name NVARCHAR(100) NOT NULL,
	address NVARCHAR(100) NOT NULL
	CONSTRAINT [pk_courseId] PRIMARY KEY (course_id)

)
CREATE TABLE leagues(
	league_id int IDENTITY(1,1) NOT NULL,
	league_name NVARCHAR(100) NOT NULL,
	organizer_id int NOT NULL,
	course_id int NOT NULL

	CONSTRAINT [pk_leagueId] PRIMARY KEY (league_id),
	CONSTRAINT [fk_organizerId] FOREIGN KEY (organizer_id) REFERENCES users(user_id),
	CONSTRAINT [fk_courseId] FOREIGN KEY (course_id) REFERENCES courses(course_id)
)
CREATE TABLE matches (
	match_id  int IDENTITY (1,1) NOT NULL,
	match_name NVARCHAR(100) NOT NULL,
	league_id  int NOT NULL,
	start_time SMALLDATETIME NOT NULL
	CONSTRAINT [pk_matchId] PRIMARY KEY (match_id)
	CONSTRAINT [fk_league_id_match] FOREIGN KEY(league_id) REFERENCES leagues(league_id)

)
CREATE TABLE user_league(
	user_id int NOT NULL,
	league_id int NOT NULL
	CONSTRAINT [pk_user_league] PRIMARY KEY (user_id, league_id)
	CONSTRAINT [fk_user_id] FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT [fk_league_id] FOREIGN KEY (league_id) REFERENCES leagues(league_id)
)
CREATE TABLE user_match(
	user_id int NOT NULL,
	match_id int NOT NULL,
	score int NULL,
	par int NULL,
	tee_time SMALLDATETIME NOT NULL
	CONSTRAINT [pk_user_match] PRIMARY KEY (user_id, match_id),
	CONSTRAINT [fk_user_id_match] FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT [fk-match_id] FOREIGN KEY (match_id) REFERENCES matches(match_id),
)
CREATE TABLE invites (
	invite_id int NOT NULL IDENTITY(1,1),
	invite_status varchar(30) NOT NULL,
	to_user int NOT NULL,
	to_league int NOT NULL
	CONSTRAINT [pk_invite] PRIMARY KEY (invite_id),
	CONSTRAINT [fk_to_user] FOREIGN KEY(to_user) REFERENCES users(user_id),
	CONSTRAINT [fk_league_invite] FOREIGN KEY(to_league) REFERENCES leagues(league_id)
)


--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=','Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('TigerWoods','kymYFm1jEQxfrXVLz8X96BV0xXQ=','EqJbtQsHdi0=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('DustinJohnson','4IY653VnLq7Ehw2AlWqGQkLjrB8=','Sn6HyNTM2Ps=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('JustinSpieth','pPj86g6sZjbM6hIuH5An9kj+yJA=','fHW9pusmFTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('DanSchneid','Xep9nSbkHc6YDRrtWTUrp67fC0I=','60qfXu+4hRU=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('RyanHering','0F3P7O+04gwnXM9QWj0p4iCcWJM=','3TyzbEEROUM=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('TaylorDupont','4UPnM3FIxhdNY0onGLHHDX3ruwo=','Ix0X9riaEXk=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('MarioRizzardi','PnIH9SznJ5cDob71KGnIvsshfvc=','C41wgmelbko=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('JohnSavage','mAsyp486DDFHqlLQaewVGGMHW+Q=','GL1jn1EHnSo=','admin');

INSERT INTO courses (course_name, address) VALUES ('Manakiki Golf Course', '35501 Eddy Rd, Willoughby, OH 44094');
INSERT INTO courses (course_name, address) VALUES ('Pebble Beach Golf Links', '1700 17 Mile Dr, Pebble Beach, CA 93953');
INSERT INTO courses (course_name, address) VALUES ('Augusta National Golf Club', '2604 Washington Rd, Augusta, GA 30904');

INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('All Users', 2, 2);
INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('The Masters', 3, 3);
INSERT INTO leagues (league_name, organizer_id, course_id) VALUES ('Tech Elevator League', 10, 1);

INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round One', 2, '2022-04-07 7:30:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Two', 2, '2022-04-08 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Three', 2, '2022-04-09 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Masters Round Four', 2, '2022-04-10 8:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Graduation Day', 3, '2022-04-22 9:00:00 AM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Caddyshack Clash', 3, '1980-07-25 12:00:00 PM');
INSERT INTO matches (match_name, league_id, start_time) VALUES ('Happy Gilmatch', 3, '1996-02-16 10:00:00 AM');

INSERT INTO user_league (user_id, league_id) VALUES (3, 1), (4, 1), (5, 1), (6, 1), (7, 1), (8, 1), (9, 1), (10, 1);
INSERT INTO user_league (user_id, league_id) VALUES (3, 2), (4, 2), (5, 2);
INSERT INTO user_league (user_id, league_id) VALUES (10, 3), (6, 3), (7, 3), (8, 3), (9, 3);

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


--CREATE TABLE league_match(
--	league_id int NOT NULL,
--	match_id int NOT NULL,
--	CONSTRAINT [pk_league_match] PRIMARY KEY (league_id, match_id),
--	CONSTRAINT [fk_league_id_match] FOREIGN KEY(league_id) REFERENCES leagues(league_id),
--	
--)
--populate default data
--INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
--INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
--INSERT INTO courses (course_name, address) VALUES ('TEST COURSE 1', '1234 Test Lane Cleveland, OH');
--INSERT INTO leagues(league_name,organizer_id,course_id) VALUES ('TEST LEAGUE 1', 2, 1);
--INSERT INTO courses (course_name, address) VALUES ('Wildcard Golf National Challenge', '1234 Test Lane Cleveland, OH');
--INSERT INTO leagues (league_name,organizer_id,course_id) VALUES ('All Users',2,2);
--INSERT INTO invites (invite_status, to_user, to_league) VALUES ('pending', 1, 1); 
-- UNCOMMENT AND RUN LINES 82-86 AFTER CREATING 3rd USER PER INSTRUCTIONS ON MERGE REQUEST

--INSERT INTO user_league (user_id, league_id) VALUES (1,1), (3,1), (2,1);
--INSERT INTO user_match (user_id, match_id, score, tee_time) VALUES
--(1, 1, 0, '2022-04-20 12:05:00'),
--(2, 1, 0, '2022-04-20 12:20:00'),
--(3, 1, 0, '2022-04-20 12:35:00');
GO

