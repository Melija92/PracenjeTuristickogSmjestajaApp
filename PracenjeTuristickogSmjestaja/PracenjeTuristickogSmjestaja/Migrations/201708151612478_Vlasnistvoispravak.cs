namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vlasnistvoispravak : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vlasnistvo", "UdioUVlasnistvu", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vlasnistvo", "UdioUVlasnistvu", c => c.String());
        }
    }
}
