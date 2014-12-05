USE m12nvv;
GO

IF OBJECT_ID('dbo.GetAllOfColor', 'IF') IS NOT NULL DROP FUNCTION dbo.GetAllOfColor
GO

CREATE FUNCTION GetAllOfColor(@color varchar(1))
RETURNS TABLE
AS
RETURN
(
	WITH figuresOnBoard(color, type, id)
	AS (SELECT Figures.color AS color, Figures.type AS type, Figures.cid AS id FROM Figures, Chessboard WHERE Chessboard.cid = Figures.cid)
	SELECT * FROM figuresOnBoard WHERE color = @color
);