USE m12nvv;
GO
SELECT figuresOnBoard.type, COUNT(*) AS figureCount
FROM (SELECT Figures.type AS type FROM Chessboard, Figures WHERE Chessboar.cid = Figures.cid)
GROUP BY type
WHERE figureCount >= 2