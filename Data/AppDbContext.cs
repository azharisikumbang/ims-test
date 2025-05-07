using IMSTest.Models;
using Microsoft.EntityFrameworkCore;

namespace IMSTest.Data;

public class AppDbContext: DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

    public DbSet<Kontrak> Kontrak { get; set; } 
    
    public DbSet<Angsuran> Angsuran { get; set; }  
}