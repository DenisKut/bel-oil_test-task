using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Contexts;

  public class KinderContext : DbContext
  {
    public DbSet<Child> Children { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Educator> Educators { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<HeadOfKindergarten> Heads { get; set; }
    public DbSet<ParentInfo> ParentInfos { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public KinderContext(DbContextOptions<KinderContext> options)
      : base(options)
    {

    }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Child>(entity => {
        entity.HasOne(g => g.Group)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.GroupId)
          .OnDelete(DeleteBehavior.Restrict)
          .HasConstraintName("FK_Children_Group");

        entity.HasOne(p => p.ParentInfo)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.ParentInfoId)
          .OnDelete(DeleteBehavior.Restrict)
          .HasConstraintName("FK_Children_ParentInfo");

        entity.HasOne(x => x.Contract)
          .WithMany(c => c.Children)
          .HasForeignKey(x => x.ContractId)
          .OnDelete(DeleteBehavior.Restrict)
          .HasConstraintName("FK_Children_Contract");

        entity.HasMany(p => p.Payments)
          .WithOne(c => c.Child)
          .HasForeignKey(x => x.ChildId);
      });

      modelBuilder.Entity<Contract>(entity => {
        entity.HasOne(x => x.HeadOfKindergarten)
          .WithMany(x => x.Contracts)
          .HasForeignKey(x => x.HeadId)
          .OnDelete(DeleteBehavior.Restrict)
          .HasConstraintName("FK_Contract_HeadId");
      });

      modelBuilder.Entity<Group>(entity => {
        entity.HasOne(x => x.Educator)
          .WithMany(x => x.Groups)
          .HasForeignKey(x => x.EducatorId)
          .OnDelete(DeleteBehavior.Restrict)
          .HasConstraintName("FK_Group_EducatorId");
      });
    }
  }
