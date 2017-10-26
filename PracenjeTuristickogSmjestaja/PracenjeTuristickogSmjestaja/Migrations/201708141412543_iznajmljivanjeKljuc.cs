namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Iznajmljivanje");
            AlterColumn("dbo.Iznajmljivanje", "sifra_iznajmljivanja", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Iznajmljivanje", new[] { "sifra_iznajmljivanja", "sifra_turista", "sifra_jedinice_iznajmljivanja" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Iznajmljivanje");
            AlterColumn("dbo.Iznajmljivanje", "sifra_iznajmljivanja", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Iznajmljivanje", new[] { "sifra_iznajmljivanja", "sifra_turista", "sifra_jedinice_iznajmljivanja" });
        }
    }
}