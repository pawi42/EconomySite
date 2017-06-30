namespace Economy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EconomicContext : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Bills",
            //    c => new
            //        {
            //            BillID = c.Int(nullable: false, identity: true),
            //            DueDate = c.DateTime(nullable: false),
            //            Amount = c.Decimal(nullable: false, storeType: "money"),
            //            Description = c.String(unicode: false),
            //            CategoryID = c.Int(nullable: false),
            //            PayerID = c.Int(nullable: false),
            //            SubCategoryID = c.Int(nullable: false),
            //            RegDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BillID)
            //    .ForeignKey("dbo.Categories", t => t.CategoryID)
            //    .ForeignKey("dbo.SubCategories", t => t.SubCategoryID)
            //    .ForeignKey("dbo.Payers", t => t.PayerID)
            //    .Index(t => t.CategoryID)
            //    .Index(t => t.PayerID)
            //    .Index(t => t.SubCategoryID);
            
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            CategoryID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 500, unicode: false),
            //            Description = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.CategoryID);
            
            //CreateTable(
            //    "dbo.CategorySubcategoryRelations",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false),
            //            CategoryID = c.Int(nullable: false),
            //            SubCategoryID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ID, t.CategoryID, t.SubCategoryID })
            //    .ForeignKey("dbo.SubCategories", t => t.SubCategoryID)
            //    .ForeignKey("dbo.Categories", t => t.CategoryID)
            //    .Index(t => t.CategoryID)
            //    .Index(t => t.SubCategoryID);
            
            //CreateTable(
            //    "dbo.SubCategories",
            //    c => new
            //        {
            //            SubCategoryID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 500, unicode: false),
            //        })
            //    .PrimaryKey(t => t.SubCategoryID);
            
            //CreateTable(
            //    "dbo.Payers",
            //    c => new
            //        {
            //            PayerID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, unicode: false),
            //        })
            //    .PrimaryKey(t => t.PayerID);
            
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
            DropForeignKey("dbo.Bills", "PayerID", "dbo.Payers");
            DropForeignKey("dbo.CategorySubcategoryRelations", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategorySubcategoryRelations", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.Bills", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.Bills", "CategoryID", "dbo.Categories");
            DropIndex("dbo.MonthlyBills", new[] { "SubCategoryID" });
            DropIndex("dbo.MonthlyBills", new[] { "PayerID" });
            DropIndex("dbo.MonthlyBills", new[] { "CategoryID" });
            DropIndex("dbo.CategorySubcategoryRelations", new[] { "SubCategoryID" });
            DropIndex("dbo.CategorySubcategoryRelations", new[] { "CategoryID" });
            DropIndex("dbo.Bills", new[] { "SubCategoryID" });
            DropIndex("dbo.Bills", new[] { "PayerID" });
            DropIndex("dbo.Bills", new[] { "CategoryID" });
            DropTable("dbo.MonthlyBills");
            DropTable("dbo.Payers");
            DropTable("dbo.SubCategories");
            DropTable("dbo.CategorySubcategoryRelations");
            DropTable("dbo.Categories");
            DropTable("dbo.Bills");
        }
    }
}
