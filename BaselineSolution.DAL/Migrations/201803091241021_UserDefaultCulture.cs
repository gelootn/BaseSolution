namespace BaselineSolution.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDefaultCulture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DefaultCulture", c => c.String());
            DropColumn("dbo.Users", "Salt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Salt", c => c.String());
            DropColumn("dbo.Users", "DefaultCulture");
        }
    }
}
