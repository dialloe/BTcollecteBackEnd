using BonDeCollecte.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System.Reflection.Emit;

namespace BonDeCollecte.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 

        public DbSet<Client> Clients { get; set; }
        public DbSet<BTCollecte> BTCollectes { get; set; }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ProductOrder>()
            //   .HasKey(t => new { t.ProductId, t.OrderId });

          /*  foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }*/

        }

    }
}
