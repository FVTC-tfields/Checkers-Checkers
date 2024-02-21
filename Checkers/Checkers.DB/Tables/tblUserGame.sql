CREATE TABLE [dbo].[tblUserGame]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [GameId] INT NOT NULL, 
    [Color] VARCHAR(50) NOT NULL
)
