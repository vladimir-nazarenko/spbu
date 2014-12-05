USE m12nvv;
GO
SELECT  
	c AS cidOfWhitePawns
FROM 
	(SELECT Figures.type AS t, Figures.cid AS c, Figures.color AS col FROM Figures, Chessboard WHERE Figures.cid = Chessboard.cid) AS figuresOnBoard
WHERE
	t = 'pawn' AND col = 'w';
