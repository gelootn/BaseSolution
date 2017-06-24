namespace BaselineSolution.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSystemLanguages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Culture = c.String(),
                        Label = c.String(),
                        CreationUserId = c.Int(),
                        CreationDate = c.DateTime(),
                        ModificationUserId = c.Int(),
                        ModificationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemLanguages");
        }
    }
}
