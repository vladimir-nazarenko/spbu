USE m12nvv;
GO
SELECT  
	figuresOnBoard.c AS idsOfFiguresStartWithK
FROM 
	(SELECT Figures.type AS t, Figures.cid AS c FROM Figures, Chessboard WHERE Figures.cid = Chessboard.cid) AS figuresOnBoard
WHERE
	t LIKE '[k]%';
