using ControleDespesas.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace ControleDespesas.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Empresa Empresa { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            return RedirectToPage("./Index");
        }
    }
}