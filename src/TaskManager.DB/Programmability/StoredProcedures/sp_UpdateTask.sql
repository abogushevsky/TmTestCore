CREATE PROCEDURE [dbo].[sp_UpdateTask]
	@Id int,
	@Title nvarchar(128),
	@Details nvarchar(max),
	@DueDate datetime,
	@CategoryId int,
	@UserId nvarchar(128),
	@ModifiedTimestamp timestamp
AS
BEGIN
	--DECLARE @rowVersion timestamp;
	--SELECT @rowVersion = (SELECT ModifiedTimestamp FROM [dbo].[Tasks] WHERE Id = @Id)
	--IF @ModifiedTimestamp != @rowVersion
		--TODO: Raise error

	UPDATE [dbo].[Tasks] SET Title = @Title, Details = @Details, DueDate = @DueDate, CategoryId = @CategoryId, UserId = @UserId
	WHERE Id = @Id
	SELECT @@ROWCOUNT AS Result
END
