namespace TaskPlanningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataBaseCreationWithUserAndToDoTaks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        HighPriority = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.Users");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.ToDoTasks");
        }
    }
}
