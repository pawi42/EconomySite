namespace Economy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Economyontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyBill",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        Description = c.String(),
                        CategoryID = c.Int(nullable: false),
                        PayerID = c.Int(nullable: false),
                        SubCategoryID = c.Int(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Payer", t => t.PayerID, cascadeDelete: true)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.PayerID)
                .Index(t => t.SubCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyBill", "SubCategoryID", "dbo.SubCategory");
            DropForeignKey("dbo.MonthlyBill", "PayerID", "dbo.Payer");
            DropForeignKey("dbo.MonthlyBill", "CategoryID", "dbo.Category");
            DropIndex("dbo.MonthlyBill", new[] { "SubCategoryID" });
            DropIndex("dbo.MonthlyBill", new[] { "PayerID" });
            DropIndex("dbo.MonthlyBill", new[] { "CategoryID" });
            DropTable("dbo.MonthlyBill");
        }
    }
}
