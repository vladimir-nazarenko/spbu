USE m12nvv;
GO
SELECT col
FROM
	(SELECT Figures.color AS col, COUNT(*) AS count
	FROM Chessboard JOIN Figures ON Chessboard.cid = Figures.cid
	GROUP BY color
	) AS figuresCount,
	(SELECT MAX(count) AS value FROM 
		(SELECT COUNT(*) AS count
		FROM Chessboard JOIN Figures ON Chessboard.cid = Figures.cid
		GROUP BY color) AS counts) AS greatest
WHERE count = greatest.value
