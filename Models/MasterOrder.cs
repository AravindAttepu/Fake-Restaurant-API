using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Models;

public class MasterOrder
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int MasterID{get; set;}
public User user{get; set;}

public string UserID{get;set;}

public Restaurant restaurant{get; set;}
public int RestaurantID{get;set;}
[Precision(18,2)]
public decimal GrandTotal{get; set;}

}
