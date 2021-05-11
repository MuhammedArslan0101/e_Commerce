namespace e_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToOrderTableOrderState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderState");
        }
    }
}
