using Kaberdin_PostItNotes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaberdin_PostItNotes.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
        public DbSet<StickerModel> Stickers { get; set; }
    }
}
