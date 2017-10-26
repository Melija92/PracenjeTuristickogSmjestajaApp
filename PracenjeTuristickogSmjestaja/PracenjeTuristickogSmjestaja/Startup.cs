using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracenjeTuristickogSmjestaja.Startup))]
namespace PracenjeTuristickogSmjestaja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
