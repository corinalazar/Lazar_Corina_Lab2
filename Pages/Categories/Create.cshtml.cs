using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lazar_Corina_Lab2.Data;
using Lazar_Corina_Lab2.Models;

namespace Lazar_Corina_Lab2.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context _context;

        public CreateModel(Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
