USE SocialService;
GO

DELETE Buildings;

DBCC CHECKIDENT('Buildings', RESEED, 0)

INSERT INTO Buildings VALUES
	('Gorkovskaya', DEFAULT, DEFAULT),
	('Sennaya', 1, 0),
	('Admiralteyaskaya', 0, 1),
	('Lomonosovskaya', 1, 1);

DELETE Employees;

INSERT INTO Employees VALUES
	('Electronov', 'electricity', 0),
	('Stark', 'electricity', 3),
	('Barateon', 'plumbery', 2),
	('Gilgamesh', 'plumbery', 0),
	('Brut', 'elevators', 3),
	('Darwin', 'gas', 1),
	('Laplas', 'gas', 2)

DELETE Rooms;

INSERT INTO Rooms VALUES
	(0, 1, 0, '12345678912'),
	(0, 2, 0, '12345678912'),
	(0, 3, 1, NULL),
	(1, 1, 0, '22345678912'),
	(1, 2, 0, '22345678912'),
	(1, 3, 1, NULL),
	(2, 1, 0, '32345678912'),
	(2, 2, 0, '32345678912'),
	(2, 3, 1, NULL),
	(3, 1, 0, '42345678912'),
	(3, 2, 0, '42345678912'),
	(3, 3, 1, NULL);

INSERT INTO Tasks VALUES
	(10, 3, '2014-02-12', '2014-02-15', '2014-02-13', 'elevators', 'main elevator is broken'),
	(3, 5, '2014-05-12', '2014-05-15', '2014-05-13', 'gas', 'water boiler is broken')