CREATE PROCEDURE [dbo].[sp_GetUserCategories]
	@UserId nvarchar(128)
AS
BEGIN
	SELECT 
		c.Id AS Id, 
		c.Name AS Name, 
		u.Id AS UserId,
		u.FirstName AS UserFirstName,
		u.LastName AS UserLastName,
		c.ModifiedTimestamp as ModifiedTimestamp 
	FROM [dbo].[Categories] c
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = c.UserId
	WHERE c.UserId = @UserId
END
