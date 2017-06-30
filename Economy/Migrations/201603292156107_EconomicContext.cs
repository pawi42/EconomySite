namespace Economy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EconomicContext : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MonthlyBill");
            DropColumn("dbo.MonthlyBill", "BillID");
            AddColumn("dbo.MonthlyBill", "MonthlyBillID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MonthlyBill", "MonthlyBillID");            
            DropColumn("dbo.MonthlyBill", "DueDate");
            DropColumn("dbo.MonthlyBill", "RegDate");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MonthlyBill");
            DropColumn("dbo.MonthlyBill", "MonthlyBillID");
            AddColumn("dbo.MonthlyBill", "RegDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MonthlyBill", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MonthlyBill", "BillID", c => c.Int(nullable: false, identity: true));
            
            AddPrimaryKey("dbo.MonthlyBill", "BillID");
        }
    }
}
