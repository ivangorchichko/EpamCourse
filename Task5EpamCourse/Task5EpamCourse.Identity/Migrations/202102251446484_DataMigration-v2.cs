namespace Task5EpamCourse.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigrationv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NickName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "NickName");
        }
    }
}
