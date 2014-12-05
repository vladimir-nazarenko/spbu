use m12nvv;
GO
SELECT figuresOnBoard.x, figuresOnBoard.y, figuresOnBoard.t, figuresOnBoard.c 
FROM
	(SELECT x, y 
	FROM Figures JOIN Chessboard ON Figures.cid = Chessboard.cid 
	WHERE type = 'king' AND color = 'b'
	) AS kingsPosition,
	(SELECT Figures.color AS c, Figures.type as t, Chessboard.x as x, Chessboard.y as y
	 FROM Figures JOIN Chessboard ON Figures.cid = Chessboard.cid) AS figuresOnBoard
WHERE abs(kingsPosition.x - figuresOnBoard.x) < 3 AND abs(ascii(kingsPosition.y) - ascii(figuresOnBoard.y)) < 3;