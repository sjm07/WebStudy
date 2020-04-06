namespace WebMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addc : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Employee",
            //    c => new
            //        {
            //            ID = c.Guid(nullable: false),
            //            NAME = c.String(),
            //            AGE = c.String(),
            //            c = c.String(),
            //            CreateTime = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.ID);

            AddColumn("dbo.Employee", "c", c => c.String());


        }

        public override void Down()
        {
            // DropTable("dbo.Employee");

            DropColumn("dbo.Employee", "c");

        }
    }
}
