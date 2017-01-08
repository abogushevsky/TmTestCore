namespace TaskManager.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FieldsLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 64));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
        }
    }
}
