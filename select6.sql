USE m12nvv;
GO
SELECT figuresOnBoard.c, COUNT(*) AS figureCount
FROM (SELECT Figures.color AS c FROM Chessboard, Figures WHERE Chessboard.cid = Figures.cid) AS figuresOnBoard
GROUP BY c