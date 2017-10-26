namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredDrzava : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drzava", "NazivDrzave", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drzava", "NazivDrzave", c => c.String(maxLength: 40));
        }
    }
}
