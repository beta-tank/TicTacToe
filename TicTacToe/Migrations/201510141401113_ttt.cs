namespace TicTacToe.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CellsString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.Guid(nullable: false),
                        Player = c.String(),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Winner = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fields", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player = c.Int(nullable: false),
                        Position = c.Byte(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Moves", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Id", "dbo.Fields");
            DropIndex("dbo.Moves", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Id" });
            DropTable("dbo.Moves");
            DropTable("dbo.Games");
            DropTable("dbo.Fields");
        }
    }
}
