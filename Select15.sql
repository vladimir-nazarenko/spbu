use m12nvv;
GO
WITH distances(c, t, dist)
AS
(SELECT figuresOnBoard.t AS t, figuresOnBoard.c AS c,
		abs(kingsPosition.x - figuresOnBoard.x) + abs(ascii(kingsPosition.y) - ascii(figuresOnBoard.y)) AS dist
FROM
	(SELECT x, y 
	FROM Figures JOIN Chessboard ON Figures.cid = Chessboard.cid 
	WHERE type = 'king' AND color = 'w') AS kingsPosition,
	(SELECT Figures.color AS c, Figures.type as t, Chessboard.x as x, Chessboard.y as y
	 FROM Figures JOIN Chessboard ON Figures.cid = Chessboard.cid
	 WHERE NOT (type = 'king' AND color = 'w')) AS figuresOnBoard)
SELECT distances.t, distances.c, distances.dist
FROM
	(SELECT MIN(dist) AS value
	FROM
		distances) AS minValue,
	distances
WHERE dist = minValue.value
