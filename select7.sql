USE m12nvv;
GO
SELECT figuresOnBoard.type
FROM (SELECT Figures.type AS type FROM Chessboard, Figures WHERE Chessboar.cid = Figures.cid AND Figures.color = 'b')
GROUP BY type