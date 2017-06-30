namespace Economy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EconomicContext : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bill", newName: "Bills");
            RenameTable(name: "dbo.Category", newName: "Categories");
            RenameTable(name: "dbo.CategorySubcategoryRelation", newName: "CategorySubcategoryRelations");
            RenameTable(name: "dbo.SubCategory", newName: "SubCategories");
            RenameTable(name: "dbo.Payer", newName: "Payers");
            CreateTable(
                "dbo.MonthlyBills",
                c => new
                    {
                        MonthlyBillID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        Description = c.String(),
                        CategoryID = c.Int(nullable: false),
                        PayerID = c.Int(nullable: false),
                        SubCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyBillID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Payers", t => t.PayerID, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.PayerID)
                .Index(t => t.SubCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonthlyBills", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.MonthlyBills", "PayerID", "dbo.Payers");
            DropForeignKey("dbo.MonthlyBills", "CategoryID", "dbo.Categories");
            DropIndex("dbo.MonthlyBills", new[] { "SubCategoryID" });
            DropIndex("dbo.MonthlyBills", new[] { "PayerID" });
            DropIndex("dbo.MonthlyBills", new[] { "CategoryID" });
            DropTable("dbo.MonthlyBills");
            RenameTable(name: "dbo.Payers", newName: "Payer");
            RenameTable(name: "dbo.SubCategories", newName: "SubCategory");
            RenameTable(name: "dbo.CategorySubcategoryRelations", newName: "CategorySubcategoryRelation");
            RenameTable(name: "dbo.Categories", newName: "Category");
            RenameTable(name: "dbo.Bills", newName: "Bill");
        }
    }
}
