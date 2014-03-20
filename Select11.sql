USE m12nvv;
GO
SELECT id
FROM
	(SELECT Chessboard.x AS rX, Chessboard.y AS rY
	FROM Chessboard, Figures 
	WHERE Chessboard.cid = Figures.cid AND Figures.type = 'rock'
	) AS rocks JOIN
	(SELECT Figures.cid as id, Chessboard.x AS fX, Chessboard.y AS fY
	FROM Chessboard JOIN Figures ON Figures.cid = Chessboard.cid) AS figuresOnRocksPath
	ON (rX = fX OR rY = fY) AND NOT (rX = fX AND rY = fY)
	

