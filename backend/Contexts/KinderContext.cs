using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.Contexts;

  public class KinderContext : IdentityDbContext
  {
    public DbSet<Child> Children { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Educator> Educators { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<HeadOfKindergarten> Heads { get; set; }
    public DbSet<ParentInfo> ParentInfos { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Zone> Zone { get; set; }

    public KinderContext(DbContextOptions<KinderContext> options)
      : base(options)
    {
      //Настройка Timestamp для работы с датой в запросах

      AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Child>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Contract>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Educator>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Group>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<HeadOfKindergarten>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<ParentInfo>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<Payment>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();


      modelBuilder.Entity<Child>(entity => {
        entity.HasOne(g => g.Group)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.GroupId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Children_Group");

        entity.HasOne(p => p.ParentInfo)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.ParentInfoId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Children_ParentInfo");

        entity.HasOne(x => x.Contract)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.ContractId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Children_Contract");

        entity.HasMany(p => p.Payments)
          .WithOne(c => c.Child)
          .HasForeignKey(x => x.ChildId);
      });

      modelBuilder.Entity<Contract>(entity => {
        entity.HasOne(x => x.HeadOfKindergarten)
          .WithMany(x => x.Contracts)
          .HasForeignKey(x => x.HeadId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Contract_HeadId");
      });

      modelBuilder.Entity<Group>(entity => {
        entity.HasOne(x => x.Educator)
          .WithMany(x => x.Groups)
          .HasForeignKey(x => x.EducatorId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Group_EducatorId");
      });
    }

  internal Task<BaseEntity> FirstOrDefaultAsync(Func<object, bool> value)
  {
    throw new NotImplementedException();
  }
}
