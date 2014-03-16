USE m12nvv;
GO
SELECT figuresOnBoard.t as type, figuresOnBoard.c as color
FROM 
     (SELECT Figures.type as t, Figures.color as c, Chessboard.x as x, Chessboarrd.y as y FROM Figures, Chessboard WHERE Figures.cid = Chessboard.cid) AS figuresOnBoard
WHERE ascii(y) = ascii('0') + x