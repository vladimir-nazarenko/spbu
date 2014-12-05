use m12nvv;
GO
SELECT c 
FROM
	(SELECT COUNT(*) AS count, color AS c 
	FROM Figures JOIN Chessboard ON Figures.cid = Chessboard.cid 
	WHERE type = 'pawn' 
	GROUP BY color) AS pawns
WHERE count = 8