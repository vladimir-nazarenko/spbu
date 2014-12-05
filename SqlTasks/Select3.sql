USE m12nvv;
GO

SELECT Figures.type, COUNT(*) AS count FROM Figures GROUP BY Figures.type;