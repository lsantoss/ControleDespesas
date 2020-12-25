using System.Web.Http.Description;
using System.Web.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [RequireHttps]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}