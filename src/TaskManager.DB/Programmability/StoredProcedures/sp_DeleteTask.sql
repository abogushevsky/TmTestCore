CREATE PROCEDURE [dbo].[sp_DeleteTask]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Tasks] WHERE Id = @Id
	SELECT @@ROWCOUNT AS Result
END
