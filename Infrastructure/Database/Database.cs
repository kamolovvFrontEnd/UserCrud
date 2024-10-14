using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class Data : DbContext
{
    public Data(DbContextOptions<Data> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Gadget> Gadgets { get; set; }
}