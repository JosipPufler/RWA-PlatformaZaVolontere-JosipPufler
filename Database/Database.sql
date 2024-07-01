--Log
CREATE TABLE Log(
	IDLog int primary key identity(1, 1),
	Timestamp datetime not null,
	Level int not null default 1,
	Message nvarchar(max) not null
);

-- 1-to-n Role
CREATE TABLE Role(
	IDRole int primary key identity(1, 1),
	Name nvarchar(50)
);

--1-to-n Skill set
CREATE TABLE SkillSet(
	IDSkillSet int primary key identity(1, 1),
	Name nvarchar(50)
);

--User
CREATE TABLE [dbo].[User](
	IDUser int primary key identity(1, 1),
	Username nvarchar(75) unique not null,
	PasswordHash nvarchar(256) not null,
	PasswordSalt nvarchar(256) not null,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(50) not null,
	PhoneNumber nvarchar(15) not null,
	JoinDate date not null,
	RoleID int foreign key references Role(IDRole) not null,
);

--1-to-n Project type
CREATE TABLE ProjectType(
	IDProjectType int primary key identity(1, 1),
	Name nvarchar(25) not null
);

--Primary - Project
CREATE TABLE Project(
	IDProject int primary key identity(1, 1),
	Title nvarchar(50) not null unique,
	Description nvarchar(500) not null,
	PublishDate datetime not null,
	StartDate datetime not null,
	EndDate date,
	TypeID int foreign key references ProjectType(IDProjectType) not null
);

--M-to-N-bridge Project-SkillSet
CREATE TABLE ProjectSkillSet(
	ID int primary key identity(1, 1),
	ProjectID int foreign key references Project(IDProject) on delete cascade not null,
	SkillSetID int foreign key references SkillSet(IDSkillSet) on delete cascade not null
);

--M-to-N-bridge User-SkillSet
CREATE TABLE UserSkillSet(
	ID int primary key identity(1, 1),
	UserID int foreign key references [dbo].[User](IDUser) on delete cascade not null,
	SkillSetID int foreign key references SkillSet(IDSkillSet) on delete cascade not null
);

--M-to-N Project-User
CREATE TABLE ProjectUser(
	ID int primary key identity(1, 1),
	ProjectID int foreign key references Project(IDProject) on delete cascade not null,
	UserID int foreign key references [dbo].[User](IDUser) on delete cascade not null
);
go

INSERT INTO Role (Name) VALUES
('Volunteer'),
('Admin')

INSERT INTO SkillSet (Name) VALUES
('Project management'),
('Coding skills'),
('Search and rescue')

INSERT INTO [dbo].[User] (Username, PasswordHash, PasswordSalt, FirstName, LastName, JoinDate, RoleID, Email, PhoneNumber) VALUES
('admin', 'RiQX7eB+Lh71EdUAgJuKiinjupf2eY/Glk0B+rKG8lg=', 'xTuF/IzVOCwSuW8YYBqnYw==', 'Pero', 'Peric', GetDate(), 2, 'pero@gmail.com', '+385616293045'),
('user', 'awRqP1fZbSV03KC5okr4FrZ3kUEBL7Mj/04cisCe6Ls=', 'OO4xTUCv2oe1Z7Bd4/u9hQ==', 'Mara', 'Maric', GetDate(), 1, 'mara@gmail.com', '+385616293046'),
('user2', 'awRqP1fZbSV03KC5okr4FrZ3kUEBL7Mj/04cisCe6Ls=', 'OO4xTUCv2oe1Z7Bd4/u9hQ==', 'Marko', 'Markic', GetDate(), 1, 'marko@gmail.com', '+385616293047')

INSERT INTO ProjectType(Name) VALUES
('Short project'),
('Dangerous project'),
('Long project')

INSERT INTO Project (Title, Description, PublishDate, StartDate, EndDate, TypeID)
VALUES 
('Projekt1', 'Nesto se dogada 1', GETDATE(), GETDATE(), GETDATE(), 3),
('Projekt2', 'Nesto se dogada 2', GETDATE(), GETDATE(), GETDATE(), 2),
('Projekt3', 'Nesto se dogada 3', GETDATE(), GETDATE(), null, 1)

INSERT INTO ProjectSkillSet(ProjectID, SkillSetID) VALUES
(1, 3),
(1, 2),
(1, 1),
(2, 3),
(3, 2)

INSERT INTO UserSkillSet (UserID, SkillSetID) VALUES
(2, 1),
(2, 2),
(3, 3)

INSERT INTO ProjectUser (UserID, ProjectID) VALUES
(2, 2)