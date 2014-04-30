USE m12nvv;
GO

IF OBJECT_ID('dbo.movementLogger', 'TR') IS NOT NULL
	DROP TRIGGER dbo.movementLogger;
GO

CREATE TABLE movements(cid int, x_old int, y_old varchar(1), x_new int, y_new varchar(1))
GO
CREATE TRIGGER movementLogger ON dbo.Chessboard AFTER UPDATE AS 
INSERT INTO movements SELECT inserted.cid AS cid, deleted.x AS x_old,
							 deleted.y AS y_old, inserted.x AS x_new, inserted.y AS y_new 
					  FROM inserted JOIN deleted ON inserted.cid = deleted.cid
GO

UPDATE dbo.Chessboard SET x = 3, y = 'd' WHERE x = 3 AND y = 'c';