CREATE TABLE [dbo].[tblGame]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [GameStateId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Winner] VARCHAR(50) NULL,
    [GameDate] DATETIME NOT NULL
)
