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

namespace Lazar_Corina_Lab2.Pages.Borrowings
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
            var bookList = _context.Book
 .Include(b => b.Author)
 .Select(x => new
 {
     x.Id,
     BookFullName = x.Title + " - " + x.Author.LastName + " " +
x.Author.FirstName
 });
            ViewData["BookID"] = new SelectList(bookList, "Id", "BookFullName");
        ViewData["MemberID"] = new SelectList(_context.Member, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
