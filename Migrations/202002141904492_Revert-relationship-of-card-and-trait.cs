namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Revertrelationshipofcardandtrait : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits");
            DropForeignKey("dbo.Cards", "TraitID", "dbo.Traits");
            DropForeignKey("dbo.Cards", "Trait2_TraitID", "dbo.Traits");
            DropIndex("dbo.Cards", new[] { "TraitID" });
            DropIndex("dbo.Cards", new[] { "Trait_TraitID" });
            DropIndex("dbo.Cards", new[] { "Trait2_TraitID" });
            CreateTable(
                "dbo.TraitCards",
                c => new
                    {
                        Trait_TraitID = c.Int(nullable: false),
                        Card_CardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trait_TraitID, t.Card_CardID })
                .ForeignKey("dbo.Traits", t => t.Trait_TraitID, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_CardID, cascadeDelete: true)
                .Index(t => t.Trait_TraitID)
                .Index(t => t.Card_CardID);
            
            DropColumn("dbo.Cards", "TraitID");
            DropColumn("dbo.Cards", "Trait_TraitID");
            DropColumn("dbo.Cards", "Trait2_TraitID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Trait2_TraitID", c => c.Int());
            AddColumn("dbo.Cards", "Trait_TraitID", c => c.Int());
            AddColumn("dbo.Cards", "TraitID", c => c.Int());
            DropForeignKey("dbo.TraitCards", "Card_CardID", "dbo.Cards");
            DropForeignKey("dbo.TraitCards", "Trait_TraitID", "dbo.Traits");
            DropIndex("dbo.TraitCards", new[] { "Card_CardID" });
            DropIndex("dbo.TraitCards", new[] { "Trait_TraitID" });
            DropTable("dbo.TraitCards");
            CreateIndex("dbo.Cards", "Trait2_TraitID");
            CreateIndex("dbo.Cards", "Trait_TraitID");
            CreateIndex("dbo.Cards", "TraitID");
            AddForeignKey("dbo.Cards", "Trait2_TraitID", "dbo.Traits", "TraitID");
            AddForeignKey("dbo.Cards", "TraitID", "dbo.Traits", "TraitID");
            AddForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits", "TraitID");
        }
    }
}
