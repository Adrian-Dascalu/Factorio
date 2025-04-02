using Microsoft.EntityFrameworkCore;
using tehnologiinet.Entities;

namespace tehnologiinet;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<DbContext> options) : base(options)
    {
        //add john to database
        //var user = new User { Name = "John", UserName =  "john_doe" , Loses = 0, Wins = 0};
        //var userRepository = new UserRepository(this);
        //userRepository.AddUser(user);
    }

    public DbSet<Production> Productions { get; set; }
    public DbSet<Consumption> Consumptions { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Database=factorio;Username=postgres;Password=postgres");
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Production>()
            .HasOne(p => p.Items)
            .WithMany()
            .HasForeignKey(p => p.ItemId);

        modelBuilder.Entity<Consumption>()
            .HasOne(c => c.Items)
            .WithMany()
            .HasForeignKey(c => c.ItemId);

        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.ResultItem)
            .WithMany(i => i.Recipes)
            .HasForeignKey(r => r.ResultItemId);

        modelBuilder.Entity<Ingredient>()
            .HasKey(i => new { i.RecipeId, i.IngredientItemId });

        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(i => i.RecipeId);

        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.IngredientItem)
            .WithMany()
            .HasForeignKey(i => i.IngredientItemId);
    }*/
    
}
