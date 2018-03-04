using PracenjeTuristickogSmjestaja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace PracenjeTuristickogSmjestaja.ApiControllers
{
    public class DrzavaController : ApiController
    {
        [HttpGet]
        [Route("api/drzava")]
        [Authorize]
        public HttpResponseMessage VracanjeSvihTurista()
        {
            using (ApplicationDbContext drzava = new ApplicationDbContext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, drzava.Drzava.ToList());
            }
        }
    }
}