namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testingcardrelationshipfortraits : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "TraitID1", "dbo.Traits");
            AddColumn("dbo.Cards", "TraitID2", c => c.Int());
            AddColumn("dbo.Cards", "Trait_TraitID", c => c.Int());
            CreateIndex("dbo.Cards", "TraitID2");
            CreateIndex("dbo.Cards", "Trait_TraitID");
            AddForeignKey("dbo.Cards", "TraitID2", "dbo.Traits", "TraitID");
            AddForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits", "TraitID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "Trait_TraitID", "dbo.Traits");
            DropForeignKey("dbo.Cards", "TraitID2", "dbo.Traits");
            DropIndex("dbo.Cards", new[] { "Trait_TraitID" });
            DropIndex("dbo.Cards", new[] { "TraitID2" });
            DropColumn("dbo.Cards", "Trait_TraitID");
            DropColumn("dbo.Cards", "TraitID2");
            AddForeignKey("dbo.Cards", "TraitID1", "dbo.Traits", "TraitID");
        }
    }
}
