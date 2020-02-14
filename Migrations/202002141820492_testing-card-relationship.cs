namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testingcardrelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cards", name: "TraitID", newName: "TraitID1");
            RenameIndex(table: "dbo.Cards", name: "IX_TraitID", newName: "IX_TraitID1");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cards", name: "IX_TraitID1", newName: "IX_TraitID");
            RenameColumn(table: "dbo.Cards", name: "TraitID1", newName: "TraitID");
        }
    }
}
