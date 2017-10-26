namespace PracenjeTuristickogSmjestaja.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class inicijalna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drzava",
                c => new
                    {
                        sifra_drzave = c.Int(nullable: false, identity: true),
                        NazivDrzave = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.sifra_drzave);
            
            CreateTable(
                "dbo.Mjesto",
                c => new
                    {
                        sifra_mjesta = c.Int(nullable: false, identity: true),
                        NazivMjesta = c.String(maxLength: 40),
                        sifra_drzave = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sifra_mjesta)
                .ForeignKey("dbo.Drzava", t => t.sifra_drzave, cascadeDelete: true)
                .Index(t => t.sifra_drzave);
            
            CreateTable(
                "dbo.Objekt",
                c => new
                    {
                        sifra_objekta = c.Int(nullable: false, identity: true),
                        broj_objekta_iznajmljivanja = c.String(nullable: false, maxLength: 100),
                        naziv = c.String(maxLength: 100),
                        Adresa = c.String(nullable: false, maxLength: 40),
                        PostanskiBroj = c.String(maxLength: 40),
                        sifra_tipa_objekta = c.Int(nullable: false),
                        sifra_tipa_iznajmljivaca = c.Int(nullable: false),
                        sifra_mjesta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sifra_objekta)
                .ForeignKey("dbo.Mjesto", t => t.sifra_mjesta, cascadeDelete: true)
                .ForeignKey("dbo.Tip_iznajmljivaca", t => t.sifra_tipa_iznajmljivaca, cascadeDelete: true)
                .ForeignKey("dbo.Tip_Objekta", t => t.sifra_tipa_objekta, cascadeDelete: true)
                .Index(t => t.broj_objekta_iznajmljivanja, unique: true)
                .Index(t => t.sifra_tipa_objekta)
                .Index(t => t.sifra_tipa_iznajmljivaca)
                .Index(t => t.sifra_mjesta);
            
            CreateTable(
                "dbo.Jedinica_Iznajmljivanja",
                c => new
                    {
                        sifra_jedinice_iznajmljivanja = c.Int(nullable: false, identity: true),
                        broj_jedinice_iznajmljivanja = c.String(nullable: false, maxLength: 100),
                        MaksimalanBrojLjudi = c.Int(nullable: false),
                        DnevnaCijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RazinaLuksuznosti = c.String(maxLength: 20),
                        sifra_objekta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sifra_jedinice_iznajmljivanja)
                .ForeignKey("dbo.Objekt", t => t.sifra_objekta, cascadeDelete: true)
                .Index(t => t.broj_jedinice_iznajmljivanja, unique: true)
                .Index(t => t.sifra_objekta);
            
            CreateTable(
                "dbo.Iznajmljivanje",
                c => new
                    {
                        sifra_iznajmljivanja = c.Int(nullable: false, identity:true),
                        sifra_turista = c.Int(nullable: false),
                        sifra_jedinice_iznajmljivanja = c.Int(nullable: false),
                        PocetakIznajmljivanja = c.DateTime(nullable: false),
                        ZavrsetakIznajmljivanja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.sifra_iznajmljivanja, t.sifra_turista, t.sifra_jedinice_iznajmljivanja })
                .ForeignKey("dbo.Jedinica_Iznajmljivanja", t => t.sifra_jedinice_iznajmljivanja, cascadeDelete: true)
                .ForeignKey("dbo.Turist", t => t.sifra_turista, cascadeDelete: true)
                .Index(t => t.sifra_turista)
                .Index(t => t.sifra_jedinice_iznajmljivanja);
            
            CreateTable(
                "dbo.Turist",
                c => new
                    {
                        sifra_turista = c.Int(nullable: false, identity: true),
                        OIB_turista = c.String(nullable: false, maxLength: 11),
                        Ime = c.String(maxLength: 40),
                        Prezime = c.String(maxLength: 40),
                        DatumRodenja = c.DateTime(nullable: false),
                        Spol = c.String(maxLength: 10),
                        sifra_mjesta = c.Int(nullable: false),
                        sifra_drzavljanstva = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sifra_turista)
                .ForeignKey("dbo.Drzavljanstvo", t => t.sifra_drzavljanstva, cascadeDelete: true)
                .ForeignKey("dbo.Mjesto", t => t.sifra_mjesta, cascadeDelete: false)
                .Index(t => t.OIB_turista, unique: true)
                .Index(t => t.sifra_mjesta)
                .Index(t => t.sifra_drzavljanstva);
            
            CreateTable(
                "dbo.Drzavljanstvo",
                c => new
                    {
                        sifra_drzavljanstva = c.Int(nullable: false, identity: true),
                        NazivDrzavljanstva = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.sifra_drzavljanstva);
            
            CreateTable(
                "dbo.Vlasnik",
                c => new
                    {
                        sifra_vlasnika = c.Int(nullable: false, identity: true),
                        OIB_vlasnika = c.String(nullable: false, maxLength: 11),
                        Ime = c.String(maxLength: 40),
                        Prezime = c.String(maxLength: 40),
                        sifra_drzavljanstva = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sifra_vlasnika)
                .ForeignKey("dbo.Drzavljanstvo", t => t.sifra_drzavljanstva, cascadeDelete: true)
                .Index(t => t.OIB_vlasnika, unique: true)
                .Index(t => t.sifra_drzavljanstva);
            
            CreateTable(
                "dbo.Vlasnistvo",
                c => new
                    {
                        sifra_vlasnika = c.Int(nullable: false),
                        sifra_objekta = c.Int(nullable: false),
                        UdioUVlasnistvu = c.String(),
                    })
                .PrimaryKey(t => new { t.sifra_vlasnika, t.sifra_objekta })
                .ForeignKey("dbo.Objekt", t => t.sifra_objekta, cascadeDelete: true)
                .ForeignKey("dbo.Vlasnik", t => t.sifra_vlasnika, cascadeDelete: true)
                .Index(t => t.sifra_vlasnika)
                .Index(t => t.sifra_objekta);
            
            CreateTable(
                "dbo.Tip_iznajmljivaca",
                c => new
                    {
                        sifra_tipa_iznajmljivaca = c.Int(nullable: false, identity: true),
                        NazivTipaIznajmljivaca = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.sifra_tipa_iznajmljivaca);
            
            CreateTable(
                "dbo.Tip_Objekta",
                c => new
                    {
                        sifra_tipa_objekta = c.Int(nullable: false, identity: true),
                        NazivTipaObjekta = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.sifra_tipa_objekta);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Objekt", "sifra_tipa_objekta", "dbo.Tip_Objekta");
            DropForeignKey("dbo.Objekt", "sifra_tipa_iznajmljivaca", "dbo.Tip_iznajmljivaca");
            DropForeignKey("dbo.Objekt", "sifra_mjesta", "dbo.Mjesto");
            DropForeignKey("dbo.Jedinica_Iznajmljivanja", "sifra_objekta", "dbo.Objekt");
            DropForeignKey("dbo.Turist", "sifra_mjesta", "dbo.Mjesto");
            DropForeignKey("dbo.Iznajmljivanje", "sifra_turista", "dbo.Turist");
            DropForeignKey("dbo.Vlasnistvo", "sifra_vlasnika", "dbo.Vlasnik");
            DropForeignKey("dbo.Vlasnistvo", "sifra_objekta", "dbo.Objekt");
            DropForeignKey("dbo.Vlasnik", "sifra_drzavljanstva", "dbo.Drzavljanstvo");
            DropForeignKey("dbo.Turist", "sifra_drzavljanstva", "dbo.Drzavljanstvo");
            DropForeignKey("dbo.Iznajmljivanje", "sifra_jedinice_iznajmljivanja", "dbo.Jedinica_Iznajmljivanja");
            DropForeignKey("dbo.Mjesto", "sifra_drzave", "dbo.Drzava");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Vlasnistvo", new[] { "sifra_objekta" });
            DropIndex("dbo.Vlasnistvo", new[] { "sifra_vlasnika" });
            DropIndex("dbo.Vlasnik", new[] { "sifra_drzavljanstva" });
            DropIndex("dbo.Vlasnik", new[] { "OIB_vlasnika" });
            DropIndex("dbo.Turist", new[] { "sifra_drzavljanstva" });
            DropIndex("dbo.Turist", new[] { "sifra_mjesta" });
            DropIndex("dbo.Turist", new[] { "OIB_turista" });
            DropIndex("dbo.Iznajmljivanje", new[] { "sifra_jedinice_iznajmljivanja" });
            DropIndex("dbo.Iznajmljivanje", new[] { "sifra_turista" });
            DropIndex("dbo.Jedinica_Iznajmljivanja", new[] { "sifra_objekta" });
            DropIndex("dbo.Jedinica_Iznajmljivanja", new[] { "broj_jedinice_iznajmljivanja" });
            DropIndex("dbo.Objekt", new[] { "sifra_mjesta" });
            DropIndex("dbo.Objekt", new[] { "sifra_tipa_iznajmljivaca" });
            DropIndex("dbo.Objekt", new[] { "sifra_tipa_objekta" });
            DropIndex("dbo.Objekt", new[] { "broj_objekta_iznajmljivanja" });
            DropIndex("dbo.Mjesto", new[] { "sifra_drzave" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tip_Objekta");
            DropTable("dbo.Tip_iznajmljivaca");
            DropTable("dbo.Vlasnistvo");
            DropTable("dbo.Vlasnik");
            DropTable("dbo.Drzavljanstvo");
            DropTable("dbo.Turist");
            DropTable("dbo.Iznajmljivanje");
            DropTable("dbo.Jedinica_Iznajmljivanja");
            DropTable("dbo.Objekt");
            DropTable("dbo.Mjesto");
            DropTable("dbo.Drzava");
        }
    }
}
