USE m12nvv;
GO

EXEC dbo.PrintChessboard
GO

DECLARE @res int

EXEC CheckPosition 2, 'e', 3, 'a', @res OUTPUT

SELECT @res
GO

SELECT * FROM GetAllOfColor('b');
GO

SELECT dbo.Eaten('w');
GO

SELECT * FROM dbo.GetAllEatable(8);
GO