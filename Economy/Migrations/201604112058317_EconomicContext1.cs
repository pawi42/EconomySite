namespace Economy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EconomicContext1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bill", newName: "Bills");
            RenameTable(name: "dbo.Category", newName: "Categories");
            RenameTable(name: "dbo.CategorySubcategoryRelation", newName: "CategorySubcategoryRelations");
            RenameTable(name: "dbo.SubCategory", newName: "SubCategories");
            RenameTable(name: "dbo.Payer", newName: "Payers");
            RenameTable(name: "dbo.MonthlyBill", newName: "MonthlyBills");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MonthlyBills", newName: "MonthlyBill");
            RenameTable(name: "dbo.Payers", newName: "Payer");
            RenameTable(name: "dbo.SubCategories", newName: "SubCategory");
            RenameTable(name: "dbo.CategorySubcategoryRelations", newName: "CategorySubcategoryRelation");
            RenameTable(name: "dbo.Categories", newName: "Category");
            RenameTable(name: "dbo.Bills", newName: "Bill");
        }
    }
}
