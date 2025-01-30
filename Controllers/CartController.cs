using FakeRestuarantAPI.Data;
using FakeRestuarantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

         public readonly ILogger<CartController> _logger;
        public readonly AppDbContext _context;

       public CartController(ILogger<CartController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    // [HttpGet("{apikey:string}")]
    // public async Task<ActionResult> getcart(string apikey)
    // {
    //     var userexists= await _context.User.FirstOrDefaultAsync(u=>u.Usercode== apikey);
    //     if(userexists!= null)
    //     {
    //         var cartitems= await _context.cart.Where(c=>c.UserID == apikey).ToListAsync();
    //         return StatusCode(200,cartitems);
    //     }
    //     else{
    //         return  StatusCode(404, new {message="no user found with given key"});
    //     }
    // }
    // [HttpPost("{apikey:string}")]
    // public async Task<ActionResult> additem(string apikey, [FromBody] setcart setcart)
    // {
      
    //     var userexists= await _context.User.FirstOrDefaultAsync(u=>u.Usercode== apikey);
    //     if(userexists!= null)
    //     { 
    //         CartDTO cartDTO = new CartDTO{
                
    //           UserID= userexists.Usercode,
    //           ItemID= setcart.item.ItemID,
    //           ItemPrice=setcart.item.ItemPrice,
    //           Quantity= setcart.Quantity,
    //           ItemName= setcart.item.ItemName

    //         };

    //     } 
    //     else{
    //         return  StatusCode(404, new {message="no user found with given key"});
    //     } 
    // }


    }
}
