USE m12nvv;
GO
IF OBJECT_ID('dbo.board1', 'U') IS NOT NULL
	DROP TABLE dbo.board1
IF OBJECT_ID('dbo.board2', 'U') IS NOT NULL
	DROP TABLE dbo.board2
GO
CREATE TABLE board1 (
	x int,
	y char(1) CHECK (y LIKE '[a-h]'),
	cid int PRIMARY KEY,
	FOREIGN KEY (cid) REFERENCES Figures(cid)
)

CREATE TABLE board2 (
	x int,
	y char(1) CHECK (y LIKE '[a-h]'),
	cid int PRIMARY KEY,
	FOREIGN KEY (cid) REFERENCES Figures(cid)
)
GO
INSERT INTO 
	board1 
VALUES
	(2, 'e', 1),
	(5, 'b', 2),
	(7, 'd', 3),
	(3, 'c', 12),
	(3, 'b', 20),
	(7, 'a', 31),
	(6, 'f', 25),
	(5, 'e', 8),
	(3, 'a', 24)
;
INSERT INTO 
	board2 
VALUES
	(3, 'e', 1),
	(5, 'b', 2),
	(7, 'd', 3),
	(8, 'b', 20),
	(7, 'a', 31),
	(5, 'e', 8),
	(3, 'h', 24)
;
SELECT board1.cid FROM 
	board1 FULL JOIN board2 ON board1.cid = board2.cid
WHERE board2.cid IS NULL OR NOT board1.x = board2.x OR NOT board1.y = board2.y;