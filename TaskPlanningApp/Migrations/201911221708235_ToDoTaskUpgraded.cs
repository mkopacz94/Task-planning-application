namespace TaskPlanningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoTaskUpgraded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "Finished");
        }
    }
}
