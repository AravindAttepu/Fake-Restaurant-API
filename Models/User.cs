using System;
using System.ComponentModel.DataAnnotations;

namespace FakeRestuarantAPI.Models;

public class User
{  
    public string UserEmail{get;set;}
    public string Password{get;set;}
    [Key]
    public string Usercode{get;set;}

}
