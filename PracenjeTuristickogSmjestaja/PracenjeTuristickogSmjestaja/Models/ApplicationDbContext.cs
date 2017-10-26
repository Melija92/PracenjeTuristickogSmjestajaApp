using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PracenjeTuristickogSmjestaja.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Drzavljanstvo> Drzavljanstvo { get; set; }
        public DbSet<Iznajmljivanje> Iznajmljivanje { get; set; }
        public DbSet<JedinicaIznajmljivanja> JedinicaIznajmljivanja { get; set; }
        public DbSet<Mjesto> Mjesto { get; set; }
        public DbSet<Objekt> Objekt { get; set; }
        public DbSet<TipIznajmljivaca> TipIznajmljivaca { get; set; }
        public DbSet<TipObjekta> TipObjekta { get; set; }
        public DbSet<Turist> Turist { get; set; }
        public DbSet<Vlasnik> Vlasnik { get; set; }
        public DbSet<Vlasnistvo> Vlasnistvo { get; set; }

        public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}