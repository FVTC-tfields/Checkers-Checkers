CREATE TABLE [dbo].[tblGameState]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Row] VARCHAR(50) NOT NULL, 
    [Column] VARCHAR(50) NOT NULL, 
    [IsKing] BIT NOT NULL
)
