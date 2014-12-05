USE SocialService;
GO

DELETE Buildings;

DBCC CHECKIDENT('Buildings', RESEED, 0)

DBCC CHECKIDENT('Employees', RESEED, 0)

DBCC CHECKIDENT('Rooms', RESEED, 0)

INSERT INTO Buildings VALUES
	('Gorkovskaya', DEFAULT, DEFAULT),
	('Sennaya', 1, 0),
	('Admiralteyaskaya', 0, 1),
	('Lomonosovskaya', 1, 1);

DELETE Employees;

INSERT INTO Employees VALUES
	('Electronov', 'electricity', 1),
	('Stark', 'electricity', 4),
	('Barateon', 'plumbery', 3),
	('Gilgamesh', 'plumbery', 1),
	('Brut', 'elevators', 4),
	('Darwin', 'gas', 2),
	('Laplas', 'gas', 3)

DELETE Rooms;

INSERT INTO Rooms VALUES
	(1, 1, 0, '12345678912'),
	(1, 2, 0, '12345678912'),
	(1, 3, 1, NULL),
	(2, 1, 0, '22345678912'),
	(2, 2, 0, '22345678912'),
	(2, 3, 1, NULL),
	(3, 1, 0, '32345678912'),
	(3, 2, 0, '32345678912'),
	(3, 3, 1, NULL),
	(4, 1, 0, '42345678912'),
	(4, 2, 0, '42345678912'),
	(4, 3, 1, NULL);

DELETE Tasks

INSERT INTO Tasks VALUES
	(4, 4, '2014-02-12', '2014-06-15', NULL, 'elevators', 'main elevator is broken'),
	(3, 5, '2014-05-12', '2014-10-15', NULL, 'gas', 'water boiler is broken')
GO

DECLARE @res int

--EXEC dbo.AddTask 'broken pipe', 7, 'plumbery', NULL, @res OUTPUT;

EXEC dbo.AddTask 'bad cable', 2, 'electricity', NULL, @res OUTPUT

SELECT @res

DECLARE @res int

EXEC dbo.FireEmployee 'Laplas', 'Darwin', @res OUTPUT

SELECT @res

EXEC dbo.Check_swap

INSERT INTO Tasks VALUES
	(8, 6, '2014-05-25', '2014-06-28', NULL, 'gas', 'broken pipe'),
	(5, 7, '2014-05-25', '2014-06-28', NULL, 'gas', 'water boiler is broken')

DEALLOCATE inserted_cursor
GO
