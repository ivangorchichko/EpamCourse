namespace Task5.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "ClientTelephone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "ClientTelephone");
        }
    }
}
