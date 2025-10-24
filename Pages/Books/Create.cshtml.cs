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
    public class CreateModel : BookCategoriesPageModel
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
            ViewData["AuthorID"] = _context.Authors.Select(a => new SelectListItem{ Value = a.ID.ToString(),
         Text = a.FirstName + " " + a.LastName}).ToList();
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }
            Book.BookCategories = newBook.BookCategories;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        
        }
    }
}
