using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Helpers
{
    public class HelperSessionContextAccessor
    {
        private IHttpContextAccessor contextAccessor;

        public HelperSessionContextAccessor
            (IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public List<Mascota> GetMascotasSession()
        {
            List<Mascota> mascotas =
                contextAccessor.HttpContext.Session.GetObject<List<Mascota>>("MASCOTASCOLLECTION");
            return mascotas;
        }
    }
}
