using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Models;

public class Order
{   
    
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int OrderID{get;set;}

public User user{get;set;}
public string UserID{get;set;}
public string ItemName{get; set;}

[Range(1,100)]
public int Quantity{get; set;}

 [Precision(10, 2)] 
public decimal ItemPrice{get;set;}

 [Precision(10, 2)] 
public decimal TotalPrice{get; set;}

public int  MasterID{get;set;}

}
