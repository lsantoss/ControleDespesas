using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ControleDespesas.Api.Controllers.Comum
{
    [RequireHttps]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public object Home()
        {
            return "Versão do Assembly da WebApi ==> " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}