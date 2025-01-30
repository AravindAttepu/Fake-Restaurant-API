using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Models;

public class DTO
{

}
public class RestaurantDTO{

   
    [Required]
    [MaxLength(255)]
    public string RestaurantName{get; set;}
    [Required]
    [MaxLength(255)]
    public string Address{get;set;}
    [Required]
    public string Type{get;set;}

    public bool ParkingLot{get;set;}

    //public string CustomerID{get;set;}
}

public class ItemDTO{

    
    [Required]
    [MaxLength(100)]
    public string ItemName{get; set;}
    [Required]
    public decimal ItemPrice{get; set;}
    [MaxLength(300)]
    public string ItemDescription{get;set;}

    public string imageUrl{get;set;}
    
}
public class GetItems{
   
    public int ItemID{get;set;}
    public string ItemName{get;set;}
    public string ItemDescription{get;set;}
    public decimal ItemPrice{get;set;}
    public string RestaurantName{get;set;}
    public int RestaurantID{get;set;}
    public string ImageUrl{get;set;}
}


public class MenuDTO{
    public List<OrderDTO> menuDTO{get;set;}
}

public class getcartDTO{
    public List<Cart> cartitems{get;set;}
    public decimal GrandTotal{get;set;}
}

public class CartDTO{
   public string UserID{get;set;}

public int ItemID{get;set;}
public string ItemName{get;set;}

[Range(1,100)]
public int Quantity{get;set;}
public decimal ItemPrice{get;set;}
public decimal TotalPrice{get;set;}
}
public class masterorders{
    public List<Order> orders{get;set;}
    
}
public class setcart{
   
public Item item{get;set;}


[Range(1,100)]
public int Quantity{get;set;}



}


public class UserDTO{
    [Required]
    [MaxLength(500)]
    [EmailAddress]
    public string UserEmail{get;set;}
    [Required]
    [MaxLength(30)]
    public string Password{get;set;}
    //public string Usercode{get;set;}
}

public class OrderDTO{
    [Required]
    public string ItemName{get; set;}
    [Required]
public int Quantity{get; set;}
}

public class setorderDTO{
    public string UserID{get;set;}
public string ItemName{get; set;}
public int Quantity{get; set;}

 [Precision(10, 2)] 
public decimal ItemPrice{get;set;}

 [Precision(10, 2)] 
public decimal TotalPrice{get; set;}

public int  MasterID{get;set;}
}

public class FullOrderDTO
{   
    

public List<Order> fullorder {get;set;}
public  decimal GrandTotal{get;set;}

}

public class MasterOrderDTO{
    public int MasterID{get;set;}
    public string UserID{get;set;}
    public int RestaurantID{get;set;}
    public decimal GrandTotal{get;set;}
}


public enum RestaurantCategory
{
    Fine_Dining,
    Bistro,
    Casual_Dining,
    Family_Style,
    Pub,
    Fast_Food,
    Buffet,
    Cafe,
    Fast_Casual,
    BBQ,
    Dhabas,
    NULL

}




