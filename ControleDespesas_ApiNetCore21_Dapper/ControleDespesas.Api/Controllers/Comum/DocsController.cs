using ControleDespesas.Api.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesas.Api.Controllers.Comum
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [TypeFilter(typeof(RequestResponseActionFilterAttribute))]
    public class DocsController : Controller
    {
        [HttpGet("docs")]
        public IActionResult Index()
        {
            return View();
        }
    }
}