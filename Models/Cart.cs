using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeRestuarantAPI.Models;

public class Cart
{
    [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int cartID{get;set;}
public User user{get;set;}
public string UserID{get;set;}
public Item item{get;set;}
public int ItemID{get;set;}
public string ItemName{get;set;}

[Range(1,100)]
public int Quantity{get;set;}
public decimal ItemPrice{get;set;}



}
