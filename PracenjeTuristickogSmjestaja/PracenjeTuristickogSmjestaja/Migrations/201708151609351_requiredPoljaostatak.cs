namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredPoljaostatak : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vlasnik", "Ime", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Vlasnik", "Prezime", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vlasnik", "Prezime", c => c.String(maxLength: 40));
            AlterColumn("dbo.Vlasnik", "Ime", c => c.String(maxLength: 40));
        }
    }
}
