namespace GymTrainingDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommonTypes",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        Domain = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Excercises",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        TrainingId = c.Decimal(nullable: false, precision: 18, scale: 0),
                        ExcerciseTypeId = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExcerciseTypes", t => t.ExcerciseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.TrainingId)
                .Index(t => t.ExcerciseTypeId);
            
            CreateTable(
                "dbo.ExcerciseTypes",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        TrainingStart = c.DateTime(nullable: false),
                        TrainingEnd = c.DateTime(nullable: false),
                        UserId = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        AccountTypeId = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        PasswordHash = c.String(),
                        LastLoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommonTypes", t => t.AccountTypeId, cascadeDelete: true)
                .Index(t => t.AccountTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AccountTypeId", "dbo.CommonTypes");
            DropForeignKey("dbo.Excercises", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.Excercises", "ExcerciseTypeId", "dbo.ExcerciseTypes");
            DropIndex("dbo.Users", new[] { "AccountTypeId" });
            DropIndex("dbo.Trainings", new[] { "UserId" });
            DropIndex("dbo.Excercises", new[] { "ExcerciseTypeId" });
            DropIndex("dbo.Excercises", new[] { "TrainingId" });
            DropTable("dbo.Users");
            DropTable("dbo.Trainings");
            DropTable("dbo.ExcerciseTypes");
            DropTable("dbo.Excercises");
            DropTable("dbo.CommonTypes");
        }
    }
}
