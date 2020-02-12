namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Traits",
                c => new
                    {
                        TraitID = c.Int(nullable: false, identity: true),
                        TraitName = c.String(),
                    })
                .PrimaryKey(t => t.TraitID);
            
            CreateTable(
                "dbo.DeckCards",
                c => new
                    {
                        Deck_DeckID = c.Int(nullable: false),
                        Card_CardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Deck_DeckID, t.Card_CardID })
                .ForeignKey("dbo.Decks", t => t.Deck_DeckID, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_CardID, cascadeDelete: true)
                .Index(t => t.Deck_DeckID)
                .Index(t => t.Card_CardID);
            
            AddColumn("dbo.Cards", "CardName", c => c.String());
            AddColumn("dbo.Cards", "Card_CardID", c => c.Int());
            AddColumn("dbo.Cards", "Trait_TraitID", c => c.Int());
            AddColumn("dbo.Series", "SeriesName", c => c.String());
            AddColumn("dbo.Series", "SeriesCode", c => c.String());
            AddColumn("dbo.Decks", "DeckName", c => c.String());
            CreateIndex("dbo.Cards", "Card_CardID");
            CreateIndex("dbo.Cards", "Trait_TraitID");
            AddForeignKey("dbo.Cards", "Card_CardID", "dbo.Cards", "CardID");
            AddForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits", "TraitID");
            DropColumn("dbo.Cards", "Name");
            DropColumn("dbo.Series", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "Code", c => c.String());
            AddColumn("dbo.Cards", "Name", c => c.String());
            DropForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits");
            DropForeignKey("dbo.Cards", "Card_CardID", "dbo.Cards");
            DropForeignKey("dbo.DeckCards", "Card_CardID", "dbo.Cards");
            DropForeignKey("dbo.DeckCards", "Deck_DeckID", "dbo.Decks");
            DropIndex("dbo.DeckCards", new[] { "Card_CardID" });
            DropIndex("dbo.DeckCards", new[] { "Deck_DeckID" });
            DropIndex("dbo.Cards", new[] { "Trait_TraitID" });
            DropIndex("dbo.Cards", new[] { "Card_CardID" });
            DropColumn("dbo.Decks", "DeckName");
            DropColumn("dbo.Series", "SeriesCode");
            DropColumn("dbo.Series", "SeriesName");
            DropColumn("dbo.Cards", "Trait_TraitID");
            DropColumn("dbo.Cards", "Card_CardID");
            DropColumn("dbo.Cards", "CardName");
            DropTable("dbo.DeckCards");
            DropTable("dbo.Traits");
        }
    }
}
