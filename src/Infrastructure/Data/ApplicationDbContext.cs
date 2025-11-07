using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SellPoint> SellPoints { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ItemCart> ItemCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Acá seteas la relación entre Product - Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<User>("User")
                .HasValue<Admin>("Admin")
                .HasValue<Client>("Client")
                .HasValue<Employee>("Employee");

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCart>().HasKey(ic => new { ic.CartId, ic.ProductId });

            modelBuilder.Entity<ItemCart>()
                .HasOne(ic => ic.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ic => ic.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemCart>()
                .HasOne(ic => ic.Product)
                .WithMany(p => p.ItemCarts)
                .HasForeignKey(ic => ic.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Query)
                .WithOne(q => q.Cart)
                .HasForeignKey<Query>(q => q.CartId)
                .IsRequired(false) //la FK es opcional (0..1)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Admin>().HasData(CreateAdminDataSeed());
        }
        private Admin[] CreateAdminDataSeed()
        {
            Admin[] Admins = [
                new Admin
        {
            Id = 1,
            Name = "Luca",
            LastName = "Pisso",
            Email = "lucapisso4@gmail.com",
            PhoneNumber = "3416932072",
            Password = "luca1234",
            Adress = "Roldán",
            Role = "Admin",
        },
        new Admin
        {
            Id = 2,
            Name = "Nicolas",
            LastName = "Solans",
            Email = "nico.solans.drc@gmail.com",
            PhoneNumber = "3412173325",
            Password = "nicolas1234",
            Adress = "Rosario, centro",
            Role = "Admin",
        },
        new Admin
        {
            Id = 3,
            Name = "Lucas",
            LastName = "Luppi",
            Email = "lucasgluppi@gmail.com",
            PhoneNumber = "3412707429",
            Password = "lucas1234",
            Adress = "Rosario, zona oeste",
            Role = "Admin",
        }
            ];

            return Admins;

        }
    }
}
