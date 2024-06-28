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