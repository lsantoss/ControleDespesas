using System.Reflection;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [RequireHttps]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public object Index()
        {
            return "Versão do Assembly da WebApi ==> " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [HttpGet]
        [Route("error")]
        public object Error()
        {
            return "Ocorreu algum erro em sua solicitação!";
        }
    }
}