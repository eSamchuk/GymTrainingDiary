namespace GymTrainingDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photocontaineradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoContainers",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        BlobData = c.Binary(),
                        UserId = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Users", "PhotoContainerId", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoContainers", "Id", "dbo.Users");
            DropIndex("dbo.PhotoContainers", new[] { "Id" });
            DropColumn("dbo.Users", "PhotoContainerId");
            DropTable("dbo.PhotoContainers");
        }
    }
}
