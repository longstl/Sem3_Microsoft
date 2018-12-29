-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-12-25 14:39:03.171

-- foreign keys
ALTER TABLE Comment DROP CONSTRAINT Comment_Post;

ALTER TABLE Comment DROP CONSTRAINT Comment_Traveler;

ALTER TABLE Image DROP CONSTRAINT Image_Post;

ALTER TABLE Post DROP CONSTRAINT Post_Traveler;

ALTER TABLE Sub_Comment DROP CONSTRAINT Sub_Comment_Comment;

ALTER TABLE Sub_Comment DROP CONSTRAINT Sub_Comment_Traveler;

ALTER TABLE Tag_Post DROP CONSTRAINT Tag_Post_Post;

ALTER TABLE Tag_Post DROP CONSTRAINT Tag_Post_Tag;

ALTER TABLE Traveler DROP CONSTRAINT Traveler_Role;

ALTER TABLE Vote DROP CONSTRAINT Vote_Post;

ALTER TABLE Vote DROP CONSTRAINT Vote_Traveler;

-- tables
DROP TABLE Admin;

DROP TABLE Comment;

DROP TABLE Image;

DROP TABLE Post;

DROP TABLE Role;

DROP TABLE Sub_Comment;

DROP TABLE Tag;

DROP TABLE Tag_Post;

DROP TABLE Traveler;

DROP TABLE Vote;

-- End of file.

