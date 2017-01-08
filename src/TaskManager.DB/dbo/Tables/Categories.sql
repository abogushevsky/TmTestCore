CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(128) NOT NULL, 
    [ModifiedTimestamp] TIMESTAMP NOT NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_Categories_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
