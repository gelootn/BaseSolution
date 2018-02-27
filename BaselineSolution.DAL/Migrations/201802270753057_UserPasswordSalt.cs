namespace BaselineSolution.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPasswordSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt");
        }
    }
}
