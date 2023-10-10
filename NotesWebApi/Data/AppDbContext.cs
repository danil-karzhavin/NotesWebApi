using Microsoft.EntityFrameworkCore;
using NotesWebApi.Models;

namespace NotesWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; } // create table in db
    }
}
