namespace DeckBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "PicID", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "PicExtension");
            DropColumn("dbo.Cards", "PicID");
        }
    }
}
