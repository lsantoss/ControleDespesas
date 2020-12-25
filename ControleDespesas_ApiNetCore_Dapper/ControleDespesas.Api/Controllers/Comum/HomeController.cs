using System;
using System.Reflection;
using ElmahCore;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                return "Versão do Assembly da WebApi ==> " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                return null;
            }
        }
    }
}