namespace SHBankWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_again : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TransactionHistory");
            AlterColumn("dbo.TransactionHistory", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TransactionHistory", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TransactionHistory");
            AlterColumn("dbo.TransactionHistory", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TransactionHistory", "Id");
        }
    }
}
