using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lazar_Corina_Lab2.Data;
using Lazar_Corina_Lab2.Models;

namespace Lazar_Corina_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context _context;

        public IndexModel(Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        // Dropdown list cu autorii
        public SelectList AuthorList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedAuthorID { get; set; }
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

            //se va include Author conform cu sarcina de la lab 2
            BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                .Where(i => i.Id == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}
