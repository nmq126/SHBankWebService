namespace SHBankWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountNumber = c.String(nullable: false, maxLength: 128),
                        Balance = c.Double(nullable: false),
                        SecurityCode = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.TransactionHistory",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        SenderAccountNumber = c.String(),
                        ReceiverAccountNumber = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionHistory");
            DropTable("dbo.Account");
        }
    }
}
