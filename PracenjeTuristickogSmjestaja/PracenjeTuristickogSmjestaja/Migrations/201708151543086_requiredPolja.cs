namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class requiredPolja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mjesto", "NazivMjesta", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Objekt", "Adresa", c => c.String(maxLength: 40));
            AlterColumn("dbo.Turist", "Ime", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Turist", "Prezime", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Tip_iznajmljivaca", "NazivTipaIznajmljivaca", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Tip_Objekta", "NazivTipaObjekta", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tip_Objekta", "NazivTipaObjekta", c => c.String(maxLength: 40));
            AlterColumn("dbo.Tip_iznajmljivaca", "NazivTipaIznajmljivaca", c => c.String(maxLength: 40));
            AlterColumn("dbo.Turist", "Prezime", c => c.String(maxLength: 40));
            AlterColumn("dbo.Turist", "Ime", c => c.String(maxLength: 40));
            AlterColumn("dbo.Objekt", "Adresa", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Mjesto", "NazivMjesta", c => c.String(maxLength: 40));
        }
    }
}
