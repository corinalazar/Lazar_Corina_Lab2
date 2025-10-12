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


        public async Task OnGetAsync()
        {
            // Populăm lista de autori
            var authors = await _context.Authors
                .Select(a => new {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToListAsync();

            AuthorList = new SelectList(authors, "ID", "FullName");

            // Construim query-ul pentru cărți
            var booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .AsQueryable();

            // Aplicăm filtrul dacă e selectat un autor
            if (SelectedAuthorID.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.AuthorID == SelectedAuthorID.Value);
            }
            Book = await booksQuery.ToListAsync();

         /*   Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .ToListAsync();
         */
        }
    }
}
