CREATE PROCEDURE [dbo].[sp_DeleteCategory]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Categories] WHERE Id = @Id
	SELECT @@ROWCOUNT AS Result
END
