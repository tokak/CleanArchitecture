using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context;

public sealed class AppDbContext : IdentityDbContext<AppUser,Role,string>, IUnitOfWork
{
    //1. yöntem
    //AppDbContext context = new();
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("");
    //}

    //çağrılma şekli
    //private readonly AppDbContext _context;

    //public AppDbContext(AppDbContext context)
    //{
    //    _context = context;
    //}


    //yukarıdakinden avandajı appsettings.json dosyasındaki connection stringi almak
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    //Entities ait özelleştirmeleri yapmak için kullandıgımız alan
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefence).Assembly);

        modelBuilder.Ignore < IdentityUserLogin<string>>();
        modelBuilder.Ignore < IdentityUserRole<string>>();
        modelBuilder.Ignore < IdentityUserClaim<string>>();
        modelBuilder.Ignore < IdentityUserToken<string>>();
        modelBuilder.Ignore < IdentityRoleClaim<string>>();
        modelBuilder.Ignore < IdentityRole<string>>();
    }
       
    //{
    //    base.OnModelCreating(modelBuilder);
    //}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)            
                entry.Property(p=>p.CreatedDate)
                    .CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Modified)
                entry.Property(p => p.UpdatedDate)
                    .CurrentValue = DateTime.Now;




        }
        return base.SaveChangesAsync(cancellationToken);    
    }
}
