-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-12-25 14:39:03.171

-- tables
-- Table: Admin
CREATE TABLE Admin (
    id int  NOT NULL IDENTITY(1, 1),
    firstName nvarchar(max)  NOT NULL,
    lastName nvarchar(max)  NOT NULL,
    address nvarchar(max)  NOT NULL,
    phone nvarchar(max)  NOT NULL,
    email nvarchar(max)  NOT NULL,
    dob datetime  NOT NULL,
    password nvarchar(max)  NOT NULL,
    salt nvarchar(max)  NOT NULL,
    IsEmailVerified bit  NOT NULL,
    ActivationCode uniqueidentifier  NOT NULL,
    createdAt datetime  NULL,
    updatedAt datetime  NULL,
    CONSTRAINT Admin_pk PRIMARY KEY  (id)
);

-- Table: Comment
CREATE TABLE Comment (
    id int  NOT NULL IDENTITY(1, 1),
    comment nvarchar(max)  NOT NULL,
    Post_id int  NOT NULL,
    Traveler_id int  NOT NULL,
    createdAt datetime  NULL,
    updatedAt datetime  NULL,
    CONSTRAINT Comment_pk PRIMARY KEY  (id)
);

-- Table: Image
CREATE TABLE Image (
    id int  NOT NULL IDENTITY(1, 1),
    name nvarchar(max)  NOT NULL,
    description nvarchar(max)  NOT NULL,
    url nvarchar(max)  NOT NULL,
    createdAt datetime  NULL,
    updatedAt datetime  NULL,
    Post_id int  NOT NULL,
    CONSTRAINT Image_pk PRIMARY KEY  (id)
);

-- Table: Post
CREATE TABLE Post (
    id int  NOT NULL IDENTITY(1, 1),
    title nvarchar(max)  NOT NULL,
    content nvarchar(max)  NOT NULL,
    createdAt datetime  NULL,
    updatedAt datetime  NULL,
    Traveler_id int  NOT NULL,
    CONSTRAINT Post_pk PRIMARY KEY  (id)
);

-- Table: Role
CREATE TABLE Role (
    id int  NOT NULL IDENTITY(1, 1),
    role_name nvarchar(max)  NOT NULL,
    CONSTRAINT Role_pk PRIMARY KEY  (id)
);

-- Table: Sub_Comment
CREATE TABLE Sub_Comment (
    id int  NOT NULL IDENTITY(1, 1),
    sub_Comment nvarchar(max)  NOT NULL,
    Comment_id int  NOT NULL,
    Traveler_id int  NOT NULL,
    createdAt datetime  NULL,
    updateAt datetime  NULL,
    CONSTRAINT Sub_Comment_pk PRIMARY KEY  (id)
);

-- Table: Tag
CREATE TABLE Tag (
    id int  NOT NULL IDENTITY(1, 1),
    tag_name nvarchar(max)  NOT NULL,
    CONSTRAINT Tag_pk PRIMARY KEY  (id)
);

-- Table: Tag_Post
CREATE TABLE Tag_Post (
    id int  NOT NULL IDENTITY(1, 1),
    Post_id int  NOT NULL,
    Tag_id int  NOT NULL,
    CONSTRAINT Tag_Post_pk PRIMARY KEY  (id)
);

-- Table: Traveler
CREATE TABLE Traveler (
    id int  NOT NULL IDENTITY(1, 1),
    firstName nvarchar(max)  NOT NULL,
    lastName nvarchar(max)  NOT NULL,
    address nvarchar(max)  NOT NULL,
    phone nvarchar(max)  NOT NULL,
    email nvarchar(max)  NOT NULL,
    dob datetime  NOT NULL,
    password nvarchar(max)  NOT NULL,
    salt nvarchar(max)  NOT NULL,
    IsEmailVerified bit  NOT NULL,
    ActivationCode uniqueidentifier  NOT NULL,
    createdAt datetime  NULL,
    updatedAt datetime  NULL,
    Role_id int  NOT NULL,
    CONSTRAINT Traveler_pk PRIMARY KEY  (id)
);

-- Table: Vote
CREATE TABLE Vote (
    id int  NOT NULL IDENTITY(1, 1),
    vote int  NOT NULL,
    Post_id int  NOT NULL,
    Traveler_id int  NOT NULL,
    CONSTRAINT Vote_pk PRIMARY KEY  (id)
);

-- foreign keys
-- Reference: Comment_Post (table: Comment)
ALTER TABLE Comment ADD CONSTRAINT Comment_Post
    FOREIGN KEY (Post_id)
    REFERENCES Post (id);

-- Reference: Comment_Traveler (table: Comment)
ALTER TABLE Comment ADD CONSTRAINT Comment_Traveler
    FOREIGN KEY (Traveler_id)
    REFERENCES Traveler (id);

-- Reference: Image_Post (table: Image)
ALTER TABLE Image ADD CONSTRAINT Image_Post
    FOREIGN KEY (Post_id)
    REFERENCES Post (id);

-- Reference: Post_Traveler (table: Post)
ALTER TABLE Post ADD CONSTRAINT Post_Traveler
    FOREIGN KEY (Traveler_id)
    REFERENCES Traveler (id);

-- Reference: Sub_Comment_Comment (table: Sub_Comment)
ALTER TABLE Sub_Comment ADD CONSTRAINT Sub_Comment_Comment
    FOREIGN KEY (Comment_id)
    REFERENCES Comment (id);

-- Reference: Sub_Comment_Traveler (table: Sub_Comment)
ALTER TABLE Sub_Comment ADD CONSTRAINT Sub_Comment_Traveler
    FOREIGN KEY (Traveler_id)
    REFERENCES Traveler (id);

-- Reference: Tag_Post_Post (table: Tag_Post)
ALTER TABLE Tag_Post ADD CONSTRAINT Tag_Post_Post
    FOREIGN KEY (Post_id)
    REFERENCES Post (id);

-- Reference: Tag_Post_Tag (table: Tag_Post)
ALTER TABLE Tag_Post ADD CONSTRAINT Tag_Post_Tag
    FOREIGN KEY (Tag_id)
    REFERENCES Tag (id);

-- Reference: Traveler_Role (table: Traveler)
ALTER TABLE Traveler ADD CONSTRAINT Traveler_Role
    FOREIGN KEY (Role_id)
    REFERENCES Role (id);

-- Reference: Vote_Post (table: Vote)
ALTER TABLE Vote ADD CONSTRAINT Vote_Post
    FOREIGN KEY (Post_id)
    REFERENCES Post (id);

-- Reference: Vote_Traveler (table: Vote)
ALTER TABLE Vote ADD CONSTRAINT Vote_Traveler
    FOREIGN KEY (Traveler_id)
    REFERENCES Traveler (id);

-- End of file.

