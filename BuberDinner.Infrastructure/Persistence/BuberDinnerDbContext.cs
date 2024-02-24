using BuberDinner.Domain.MenuAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinnerDbContext : DbContext
{
    public BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options) : base(options)
    {

    }

    public DbSet<Menu> Menus { get; set; } = null!;
}