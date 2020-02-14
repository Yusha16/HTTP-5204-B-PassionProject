namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingCardRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TraitCards", "Trait_TraitID", "dbo.Traits");
            DropForeignKey("dbo.TraitCards", "Card_CardID", "dbo.Cards");
            DropIndex("dbo.TraitCards", new[] { "Trait_TraitID" });
            DropIndex("dbo.TraitCards", new[] { "Card_CardID" });
            AddColumn("dbo.Cards", "TraitID", c => c.Int());
            CreateIndex("dbo.Cards", "TraitID");
            AddForeignKey("dbo.Cards", "TraitID", "dbo.Traits", "TraitID");
            DropTable("dbo.TraitCards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TraitCards",
                c => new
                    {
                        Trait_TraitID = c.Int(nullable: false),
                        Card_CardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trait_TraitID, t.Card_CardID });
            
            DropForeignKey("dbo.Cards", "TraitID", "dbo.Traits");
            DropIndex("dbo.Cards", new[] { "TraitID" });
            DropColumn("dbo.Cards", "TraitID");
            CreateIndex("dbo.TraitCards", "Card_CardID");
            CreateIndex("dbo.TraitCards", "Trait_TraitID");
            AddForeignKey("dbo.TraitCards", "Card_CardID", "dbo.Cards", "CardID", cascadeDelete: true);
            AddForeignKey("dbo.TraitCards", "Trait_TraitID", "dbo.Traits", "TraitID", cascadeDelete: true);
        }
    }
}
