namespace Task4.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClientName = c.String(),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProductName = c.String(),
                        Price = c.Double(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Id", "dbo.Purchase");
            DropForeignKey("dbo.Client", "Id", "dbo.Purchase");
            DropIndex("dbo.Product", new[] { "Id" });
            DropIndex("dbo.Client", new[] { "Id" });
            DropTable("dbo.Product");
            DropTable("dbo.Purchase");
            DropTable("dbo.Client");
        }
    }
}
