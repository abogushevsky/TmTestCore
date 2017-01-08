CREATE PROCEDURE [dbo].[sp_CreateCategory]
	@Name nvarchar(128),
	@UserId nvarchar(128)
AS
BEGIN
	INSERT INTO [dbo].[Categories] (Name, UserId) 
	VALUES (@Name, @UserId)
	SELECT SCOPE_IDENTITY() AS Result
END
