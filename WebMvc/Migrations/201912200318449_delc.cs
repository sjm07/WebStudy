namespace WebMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "c");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "c", c => c.String());
        }
    }
}
