using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lazar_Corina_Lab2.Data;
using Lazar_Corina_Lab2.Models;

namespace Lazar_Corina_Lab2.Pages.Books
{
    public class EditModel :  BookCategoriesPageModel

    {
        private readonly Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context _context;

        public EditModel(Lazar_Corina_Lab2.Data.Lazar_Corina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
 .Include(b => b.Publisher)
 .Include(b => b.BookCategories).ThenInclude(b => b.Category)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Book);

            ViewData["AuthorID"] = new SelectList(
    _context.Authors.Select(a => new { a.ID,FullName = a.FirstName + " " + a.LastName}), "ID", "FullName", Book.AuthorID);


            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var bookToUpdate = await _context.Book.Include(i => i.Publisher).Include(i=>i.Author).Include(i => i.BookCategories).ThenInclude(i => i.Category).FirstOrDefaultAsync(s => s.Id == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            //se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Book>(bookToUpdate,"Book",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
