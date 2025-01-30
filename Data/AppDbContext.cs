using System;
using FakeRestuarantAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Data;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {

    }

    public DbSet<User> User{get; set;}
    public DbSet<Item> Item{get; set;}
    public DbSet<Order> Order{get; set;}
    public DbSet<Restaurant> Restaurant{get;set;}

    public DbSet<MasterOrder> masterOrders{get;set;}

    public DbSet<Cart> cart{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
   

modelBuilder.Entity<User>()
        .HasIndex(mo => mo.Usercode)
        .IsUnique();
modelBuilder.Entity<User>()
        .HasKey(mo => mo.UserEmail);

    base.OnModelCreating(modelBuilder);
}


}


       





