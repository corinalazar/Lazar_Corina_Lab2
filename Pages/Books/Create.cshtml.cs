using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lazar_Corina_Lab2.Data;
using Lazar_Corina_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lazar_Corina_Lab2.Pages.Books
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
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            ViewData["AuthorID"] = _context.Authors.Select(a => new SelectListItem
        {
         Value = a.ID.ToString(),
         Text = a.FirstName + " " + a.LastName
         }).ToList();
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
