using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Models;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemID{get;set;}
    public string ItemName{get; set;}
    public string ItemDescription{get;set;}
    [Precision(18, 2)] 
    public decimal ItemPrice{get; set;}
    public Restaurant restaurant{get;set;}
    public int RestaurantID{get;set;}
    
    [AllowNull]
    public string imageUrl{get;set;}

    
}
