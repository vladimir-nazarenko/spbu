USE m12nvv;
GO
SELECT figuresOnBoard.type, COUNT(*) AS figureCount
FROM (SELECT Figures.type AS type FROM Chessboard, Figures WHERE Chessboard.cid = Figures.cid AND Figures.color = 'b') AS figuresOnBoard
GROUP BY type