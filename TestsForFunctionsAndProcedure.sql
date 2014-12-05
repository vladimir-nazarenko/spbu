USE m12nvv;
GO
initChessboard;
GO

EXEC dbo.PrintChessboard
GO

DECLARE @res int

EXEC CheckPosition 2, 'e', 3, 'a', @res OUTPUT

SELECT @res
GO

SELECT * FROM GetAllOfColor('b');
GO

SELECT dbo.Eaten('w', 'pawn');
GO
initChessboard;
SELECT * FROM dbo.GetAllEatable(24);
GO