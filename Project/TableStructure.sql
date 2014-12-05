CREATE DATABASE SocialService;
GO

USE SocialService;
GO

CREATE TABLE Buildings(
	ID int PRIMARY KEY IDENTITY (0, 1),
	Adress varchar(100),
	HasLift BIT NOT NULL DEFAULT(0),
	HasIntercom BIT NOT NULL DEFAULT(0)
);

CREATE TABLE Employees(
	ID int PRIMARY KEY IDENTITY (0, 1),
	Surname varchar(20),
	Qualification varchar(20) NOT NULL,
	WorkingBuildingID int,
	FOREIGN KEY (WorkingBuildingID) REFERENCES Buildings(ID)
);

ALTER TABLE Employees ADD CONSTRAINT Qual_is_from_list
    CHECK (Qualification IN  ('electricity', 'plumbery', 'elevators', 'gas'))

CREATE TABLE Rooms(
	ID int PRIMARY KEY IDENTITY (0, 1),
	BuildingID int,
	Number int,
	IsTechincal BIT,
	PhoneNumber char(11),
	FOREIGN KEY (BuildingID) REFERENCES Buildings(ID)
);

CREATE INDEX RoomsByBuilding ON Rooms(BuildingID)
CREATE INDEX EmpsByDuilding ON Employees(WorkingBuildingID);

CREATE TABLE Tasks(
	ID int PRIMARY KEY IDENTITY (0, 1),
	RoomID int,
	EmployeeID int,
	AssignDate DATE DEFAULT GETDATE(),
	DeadlineDate DATE,
	FinishedDate DATE,
	Class varchar(20),
	Description varchar(200),
	FOREIGN KEY (RoomID) REFERENCES Rooms(ID),
	FOREIGN KEY (EmployeeID) REFERENCES Employees(ID),
	CHECK (DATEDIFF(DAY, AssignDate, DeadlineDate) >= 0)
);

ALTER TABLE Tasks ADD CONSTRAINT Class_is_from_list
    CHECK (Class IN  ('electricity', 'plumbery', 'elevators', 'gas'))