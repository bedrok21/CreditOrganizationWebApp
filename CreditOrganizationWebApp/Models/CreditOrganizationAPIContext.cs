using Microsoft.EntityFrameworkCore;

namespace CreditOrganizationWebApp.Models
{
    public class CreditOrganizationAPIContext :DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }  

        public virtual DbSet<Loan> Loans { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public CreditOrganizationAPIContext(DbContextOptions<CreditOrganizationAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Server=DESKTOP-KH15CQR\\SQLEXPRESS; Database=CreditOrganizationAPI; Trusted_Connection=True; MultipleActiveResultSets=true; Trust Server Certificate=True;");
    }
}
