using System.Linq.Expressions;
using FakeRestuarantAPI.Data;
using FakeRestuarantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public readonly ILogger<OrderController> _logger;
        public readonly AppDbContext _context;

       public OrderController(ILogger<OrderController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpPost("{restaurantid}/makeorder")]
    public async Task<ActionResult> setorder( int restaurantid,[FromQuery] string apikey,  [FromBody] MenuDTO menuDTO)
    {  var customerexists= await _context.User.FirstOrDefaultAsync(u=> u.Usercode == apikey);
    if(customerexists!= null)
    {
        var restaurantexists = await _context.Restaurant.FirstOrDefaultAsync(r=>r.RestaurantID == restaurantid);
        if(restaurantexists!= null)
        {

            var restaurantitems = await _context.Item.Where(i=>i.RestaurantID==restaurantid).ToListAsync();
            decimal TotalPrice=0.00m;
              FullOrderDTO orderList = new FullOrderDTO { fullorder = new List<Order>() };
              decimal GrandTotal= 0.00m;

           //extract master id 
            var masterid=_context.masterOrders.OrderByDescending(m => m.MasterID)
                                                .Select(m => m.MasterID)
                                                .FirstOrDefault();
                                                        
            foreach(var item in menuDTO.menuDTO)
            {

                TotalPrice=0.00m;
                var itemexists= restaurantitems.FirstOrDefault(i=>i.ItemName == item.ItemName);
                if(itemexists!= null)
                {
                     decimal itemPrice= (decimal)(itemexists.ItemPrice* item.Quantity);
                     TotalPrice+=itemPrice;

                   Order order = new Order{
                        UserID = customerexists.UserEmail,
                        ItemName= item.ItemName,
                        Quantity= item.Quantity,
                        ItemPrice= itemexists.ItemPrice,
                        TotalPrice= TotalPrice,
                        MasterID=masterid+1
                    
                        
                        
                    };
                    GrandTotal+=TotalPrice;
                  orderList.fullorder.Add(order);
                  //   return Ok(new { OrderID = order.OrderID }); 
                }
                else{
                return StatusCode(400,new{message="The Item did not exists on restaurant menu"});
                }
            }
             //genrate master is
             MasterOrder masterOrder= new MasterOrder{
                UserID= customerexists.UserEmail,
                GrandTotal= GrandTotal,
                RestaurantID= restaurantexists.RestaurantID,
              
                

            };
            orderList.GrandTotal=GrandTotal;
            await _context.masterOrders.AddAsync(masterOrder);
            await _context.SaveChangesAsync();

            foreach(var order in orderList.fullorder)
            {
                await _context.Order.AddAsync(order);
                
            }
            await _context.SaveChangesAsync();
           
            


            return StatusCode(201, orderList);
        }
        else{
             return StatusCode(400,new{message=$"No Restaurant Exists with {restaurantid}"});
        }

    }
    else{
        return StatusCode(401,new{message="Invalid API key"});
    }
    }



    //[HttpPatch]
    
    [HttpGet]
    public async Task<ActionResult> getorders([FromQuery] string apikey )
    {
        var customerexists= await _context.User.FirstOrDefaultAsync(u=> u.Usercode == apikey);
    if(customerexists!= null)
    {
        var totalorders= await _context.masterOrders.Where(m=>m.UserID == customerexists.UserEmail).ToListAsync();
       var totalorders2= await _context.Order.Where(o=>o.UserID== customerexists.UserEmail).ToListAsync();


        var masterorders= totalorders.Select(t=>new {
            masterID=t.MasterID,
            userID=t.UserID,
            usercode=t.user.Usercode,
            RestaurantID= t.RestaurantID,
            Grandtotal=t.GrandTotal
            
            
        });
          return StatusCode(200,masterorders);

    }
    else{
        return StatusCode(401,new{message="Invalid API key"});
    }
    
    }

    [HttpDelete("{Order_id}" )]
    public async Task<ActionResult> deleteorder([FromRoute] int Order_id, string apikey )
    {
         var customerexists= await _context.User.FirstOrDefaultAsync(u=> u.Usercode == apikey);
    if(customerexists!= null)
    {
        var orderexits = await _context.Order.FirstOrDefaultAsync(o=>o.OrderID == Order_id);
        if(orderexits!= null)
        {
            _context.Order.Remove(orderexits);
            await _context.SaveChangesAsync();
            return StatusCode(200, new{message ="Order Deleted",orderexits});
        }
        else
        {
            return StatusCode(400,new{messge="Orders are not found with associated ID"});
        }
    }
     else{
        return StatusCode(401,new{message="Invalid API key"});
    }
    }

     [HttpDelete("master/{master_id}" )]
    public async Task<ActionResult> master([FromRoute] int master_id, string apikey )
    {
         var customerexists= await _context.User.FirstOrDefaultAsync(u=> u.Usercode == apikey);
    if(customerexists!= null)
    {
        var orderexits = await _context.masterOrders.Include(o=>o.restaurant).Where(o=>o.MasterID == master_id).ToListAsync();
        if(orderexits!= null)
        {
            _context.masterOrders.RemoveRange(orderexits);
            var singleorders= await _context.Order.Where(o=>o.MasterID == master_id).ToListAsync();
            _context.Order.RemoveRange(singleorders);
            await _context.SaveChangesAsync();
            return StatusCode(200, new{message ="Master order Deleted",orderexits,singleorders});
        }
        else
        {
            return StatusCode(400,new{messge="Orders are not found with associated ID"});
        }
    }
     else{
        return StatusCode(401,new{message="Invalid API key"});
    }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> getordersbyid([FromQuery] string apikey,int id )
    {
 var customerexists= await _context.User.FirstOrDefaultAsync(u=> u.Usercode == apikey);
    if(customerexists!= null)
    {

        var totalorders= await _context.Order.Where(o=>o.MasterID== id).ToListAsync();
        
        
        return StatusCode(200,totalorders);

    }
     else
     {
        return StatusCode(401,new{message="Invalid API key"});
    }


    }

    
}
}
