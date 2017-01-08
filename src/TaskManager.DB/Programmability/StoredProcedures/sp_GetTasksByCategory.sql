CREATE PROCEDURE [dbo].[sp_GetTasksByCategory]
	@CategoryId int
AS
BEGIN
	SELECT 
		t.Id AS Id, 
		t.Title AS Title,
		t.Details AS Details,
		t.DueDate AS DueDate,
		u.Id AS UserId,
		u.FirstName AS UserFirstName,
		u.LastName AS UserLastName,
		t.CategoryId AS CategoryId,
		c.Name AS CategoryName,
		t.ModifiedTimestamp as ModifiedTimestamp
	FROM [dbo].[Tasks] t
	INNER JOIN [dbo].[AspNetUsers] u ON u.Id = t.UserId
	LEFT JOIN [dbo].[Categories] c ON c.Id = t.CategoryId
	WHERE t.UserId = @CategoryId
END
