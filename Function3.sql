USE m12nvv;
GO

IF OBJECT_ID('dbo.GetAllEatable', 'TF') IS NOT NULL DROP FUNCTION dbo.GetAllEatable;
GO

--WORKS JUST FOR ROOK!
CREATE FUNCTION GetAllEatable(@id int)
RETURNS @eatable TABLE
(
	cid int,
	x int,
	y varchar(1),
	type varchar(10),
	color varchar(1)
)
AS
BEGIN
	-- Store x and y of input, diff is temporary variable
	DECLARE @fx int, @fy varchar(1), @diff int
	DECLARE @figuresToCheck TABLE(x int, y varchar(1), type varchar(1), color varchar(1), id int)
	SET @fx = (SELECT x FROM Chessboard WHERE cid = @id);
	SET @fy = (SELECT y FROM Chessboard WHERE cid = @id);
	-- insert all possible variants
	INSERT INTO @figuresToCheck SELECT x, y, type, color, Figures.cid FROM Chessboard FULL JOIN Figures ON Chessboard.cid = Figures.cid WHERE x = @fx AND NOT y = @fy

	--If exists figure in the column at bottom
	SET @diff = (SELECT MAX(ASCII(y) - ASCII(@fy)) FROM @figuresToCheck WHERE y < @fy)
	IF @diff < 0 INSERT INTO @eatable SELECT * FROM @figuresToCheck WHERE y = @fy + @diff

	--If exists figure in the column at above
	SET @diff = (SELECT MIN(ASCII(y) - ASCII(@fy)) FROM @figuresToCheck WHERE y > @fy)
	IF @diff > 0 INSERT INTO @eatable SELECT * FROM @figuresToCheck WHERE y = @fy - @diff
	
	--If exists figure in the row at right
	SET @diff = (SELECT MIN(x - @fx) FROM @figuresToCheck WHERE x > @fx)
	IF @diff < 0 INSERT INTO @eatable SELECT * FROM @figuresToCheck WHERE x = @fx + @diff

	--If exists figure in the row at right
	SET @diff = (SELECT MAX(x - @fx) FROM @figuresToCheck WHERE x < @fx)
	IF @diff > 0 INSERT INTO @eatable SELECT * FROM @figuresToCheck WHERE x = @fx - @diff
	RETURN
END;