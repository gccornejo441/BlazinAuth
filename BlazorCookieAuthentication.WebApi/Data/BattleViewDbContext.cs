using BlazorCookieAuthentication.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorCookieAuthentication.WebApi.Data
{
    public partial class BattleViewDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BattleViewDbContext(DbContextOptions<BattleViewDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Maintainer> Maintainers { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Accountid).HasName("account_pkey");

                entity.ToTable("account");

                entity.Property(e => e.Accountid).HasColumnName("accountid");
                entity.Property(e => e.Accountname)
                    .HasMaxLength(255)
                    .HasColumnName("accountname");
                entity.Property(e => e.Createddate).HasColumnName("createddate");
            });

            modelBuilder.Entity<Maintainer>(entity =>
            {
                entity.HasKey(e => e.Maintainerid).HasName("maintainer_pkey");

                entity.ToTable("maintainer");

                entity.Property(e => e.Maintainerid).HasColumnName("maintainerid");
                entity.Property(e => e.Accountid).HasColumnName("accountid");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.Maintainername)
                    .HasMaxLength(255)
                    .HasColumnName("maintainername");

                entity.HasOne(d => d.Account).WithMany(p => p.Maintainers)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_maintainer_account");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Roleid).HasName("Role_pkey");

                entity.ToTable("Role");

                entity.Property(e => e.Roleid).HasColumnName("roleid");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Rolename)
                    .HasMaxLength(255)
                    .HasColumnName("rolename");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Userid).HasName("User_pkey");

                entity.ToTable("User");

                entity.Property(e => e.Userid).HasColumnName("userid");
                entity.Property(e => e.Accountid).HasColumnName("accountid");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Roleid).HasColumnName("roleid");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Account).WithMany(p => p.Users)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_account");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}