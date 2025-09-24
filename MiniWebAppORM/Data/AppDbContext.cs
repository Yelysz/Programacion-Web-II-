using Microsoft.EntityFrameworkCore;
using MiniWebAppORM.Models;

namespace MiniWebAppORM.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Usuario> Usuarios { get; set; } = default!;
}