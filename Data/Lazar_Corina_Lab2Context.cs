using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lazar_Corina_Lab2.Models;

namespace Lazar_Corina_Lab2.Data
{
    public class Lazar_Corina_Lab2Context : DbContext
    {
        public Lazar_Corina_Lab2Context (DbContextOptions<Lazar_Corina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Lazar_Corina_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Lazar_Corina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Lazar_Corina_Lab2.Models.Author> Authors { get; set; } = default!;
        public DbSet<Lazar_Corina_Lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<Lazar_Corina_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Lazar_Corina_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
