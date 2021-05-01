using ControleDespesas.Api.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [TypeFilter(typeof(RequestResponseActionFilter))]
    [Route("docs")]
    public class DocsController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}