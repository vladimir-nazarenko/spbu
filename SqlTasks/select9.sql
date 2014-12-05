USE m12nvv;
GO
SELECT * FROM (
	SELECT figuresOnBoard.type, COUNT(*) AS figureCount
	FROM (
		SELECT Figures.type AS type 
		FROM Chessboard, Figures 
		WHERE Chessboard.cid = Figures.cid
	) AS FiguresOnBoard
	GROUP BY type) AS types
WHERE types.figureCount >= 2