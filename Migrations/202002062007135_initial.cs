namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CardColour = c.String(),
                        SeriesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.Series", t => t.SeriesID, cascadeDelete: true)
                .Index(t => t.SeriesID);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        SeriesID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.SeriesID);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DeckID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "SeriesID", "dbo.Series");
            DropIndex("dbo.Cards", new[] { "SeriesID" });
            DropTable("dbo.Decks");
            DropTable("dbo.Series");
            DropTable("dbo.Cards");
        }
    }
}
