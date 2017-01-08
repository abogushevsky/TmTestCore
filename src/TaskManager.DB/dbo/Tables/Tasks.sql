CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1, 1), 
    [Title] NVARCHAR(128) NOT NULL, 
    [Details] NVARCHAR(MAX) NULL, 
    [DueDate] DATETIME NULL, 
    [CategoryId] INT NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [ModifiedTimestamp] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_Tasks_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_Tasks_Users] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
