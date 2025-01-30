using System.Linq.Expressions;
using FakeRestuarantAPI.Data;
using FakeRestuarantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Controllers
{
    [Route("api/Restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        public readonly ILogger<RestaurantController> _logger;
        public readonly AppDbContext _context;
        public readonly string baseurl="https://fakerestaurantapi.runasp.net/images/";

       public RestaurantController(ILogger<RestaurantController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> getrestaurants([FromQuery] string category="", string address=null, string name=null)
    {
        //ascending
        var restaurants= await _context.Restaurant.OrderBy(r=>r.RestaurantName).ToListAsync();
        if(restaurants.Any())
        {
            if(!string.IsNullOrEmpty(name))
            {
                restaurants= restaurants.Where(r=>r.RestaurantName.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
            }
             if (!string.IsNullOrEmpty(address))
                {
                    restaurants = restaurants.Where(r => r.Address.Contains(address, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            if(category!= null && category != "")
                {
                    restaurants= restaurants.Where(r=>r.Type == category).ToList();
                }


            
            var all_restaurants =  restaurants.Select(r=>new Restaurant{
                RestaurantID= r.RestaurantID,
                RestaurantName= r.RestaurantName,
                Type=r.Type,
                Address=r.Address,
                ParkingLot=r.ParkingLot
            }).ToList();

            return Ok(all_restaurants);
        }
        else{
            return StatusCode(404,"No Restaurants Found");
        }
    }

    [HttpPost]
    public async Task<ActionResult> addrestaurant( RestaurantDTO restaurantDTO)
    {

        

        var restaurant_exists= await _context.Restaurant.AnyAsync(r=>r.RestaurantName == restaurantDTO.RestaurantName);
        if(!restaurant_exists)
        {
             Restaurant new_restaurant= new Restaurant(){
            RestaurantName = restaurantDTO.RestaurantName,
            Address= restaurantDTO.Address,
            Type= restaurantDTO.Type,
            ParkingLot=restaurantDTO.ParkingLot
           
        };
        await _context.Restaurant.AddAsync(new_restaurant);
        
        return StatusCode(201, new_restaurant);


        }
        return Conflict(new{message="Restaurant Already Exists"});
        
        

    }

     [HttpGet("{Restaurant_id}")]
     public async Task<ActionResult> getrestaurantbyid([FromRoute] int Restaurant_id)
     {
        var restaurant = await  _context.Restaurant.FirstOrDefaultAsync(r => r.RestaurantID == Restaurant_id);
        if(restaurant != null)
        {
            return Ok(restaurant);
        }
        return StatusCode(404, $"No Restaurant Exists with {Restaurant_id}");
     }



        [HttpGet("{Restaurant_id}/menu")]
        public async Task<ActionResult> getmenu(int Restaurant_id,[FromQuery] string sortbyprice = "")
        {
            var restaurant_exists= await _context.Restaurant.FirstOrDefaultAsync(r=>r.RestaurantID ==Restaurant_id);
            if(restaurant_exists!= null)
            {
                //retrieve items from restaurant menu
                var items= await _context.Item.Include(i => i.restaurant).Where(i=>i.RestaurantID==Restaurant_id).ToListAsync();

                //descending by price
                if(string.Equals(sortbyprice,"desc",StringComparison.OrdinalIgnoreCase))
                {
                    items= items.OrderByDescending(i=>i.ItemPrice).ToList();
                }

                //ascending by price
                 if(string.Equals(sortbyprice,"asc",StringComparison.OrdinalIgnoreCase))
                {
                    items= items.OrderBy(i=>i.ItemPrice).ToList();
                }

                var menu = items.Where(i=>i.RestaurantID== Restaurant_id).Select(i=>new GetItems{
                    ItemID= i.ItemID,
                     ItemName= i.ItemName,
                    ItemDescription=i.ItemDescription,
                    ItemPrice=i.ItemPrice,
                    RestaurantName=i.restaurant.RestaurantName,
                    RestaurantID= Restaurant_id,
                    ImageUrl=$"{baseurl}{i.imageUrl}"
                   // restaurant= restaurant_exists
                   
                   
                }).ToList();

                return Ok(menu);
                

            }
            return StatusCode(404, $"No Restaurant Exists with id:{Restaurant_id}");
        }

        [HttpPost("{Restaurant_id}/additem")]
        public async Task<ActionResult> setmenu(int Restaurant_id, [FromBody] ItemDTO itemDTO )
        {

             var restaurant_exists= await _context.Restaurant.FirstOrDefaultAsync(r=>r.RestaurantID ==Restaurant_id);
            if(restaurant_exists!= null)
            {

           Item newitem=new Item{
           
            ItemName= itemDTO.ItemName,
            ItemDescription= itemDTO.ItemDescription,
            ItemPrice= itemDTO.ItemPrice,
            RestaurantID= Restaurant_id,
            imageUrl= itemDTO.imageUrl
           

           };
           await _context.Item.AddAsync(newitem);
           
            return StatusCode(201, newitem);
            }

          
            return StatusCode(404, $"No Restaurant Exists with id:{Restaurant_id}");
        }

        [NonAction]
        [HttpPut]
        public async Task<ActionResult> updateimageurl(int id,string url1,string url2, string url3)
        {
           
            
                var menu = await _context.Item.Where(i=>i.RestaurantID== id).Select(i=>new Item{
                    ItemID= i.ItemID,
                     ItemName= i.ItemName,
                    ItemDescription=i.ItemDescription,
                    ItemPrice=i.ItemPrice,
                    RestaurantID= id,
                    imageUrl=$"{baseurl}{i.imageUrl}"
                }).ToListAsync();
                int i=0;
                  var urls = new string[] { url1, url2, url3 };
foreach(var item in menu)
{
    var item12 = _context.Item
        .FirstOrDefault(i => i.ItemName == item.ItemName && i.RestaurantID == id);
    if (item12 != null)
    {
        item12.imageUrl = urls[i];
    }
    i++;
}
  await _context.SaveChangesAsync();
return Ok();
            
    //         var item1 = _context.Item
    //     .FirstOrDefault(i => i.ItemName == url1 && i.RestaurantID == id);
    // if (item1 != null)
    // {
    //     item1.imageUrl = "vegetable biryani.jpg";
    // }
    //  item1 = _context.Item
    //     .FirstOrDefault(i => i.ItemName == url2 && i.RestaurantID == id);
    // if (item1 != null)
    // {
    //     item1.imageUrl = "vegetable biryani.jpg";
    // }
    //         item1 = _context.Item
    //     .FirstOrDefault(i => i.ItemName == url2 && i.RestaurantID == id);
    // if (item1 != null)
    // {
    //     item1.imageUrl = "vegetable biryani.jpg";
    // }



        }

          [HttpGet("items")]
    public async Task<ActionResult> getitems([FromQuery]  string ItemName="",string sortbyprice="")
    {
        //ascending
        var items= await _context.Item.Include(i => i.restaurant).OrderBy(r=>r.ItemName).ToListAsync();
        if(items.Any())
        {
             if (!string.IsNullOrEmpty(ItemName))
                {
                    items = items.Where(r => r.ItemName.Contains(ItemName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            if(!string.IsNullOrEmpty(sortbyprice))
                {
                    if(string.Equals(sortbyprice,"asc",StringComparison.OrdinalIgnoreCase))
                    {
                        items= items.OrderBy(i=>i.ItemPrice).ToList();
                    }
                    if(string.Equals(sortbyprice,"desc",StringComparison.OrdinalIgnoreCase))
                    {
                        items= items.OrderByDescending(i=>i.ItemPrice).ToList();
                    }
                   
                    items= items.ToList();
                }


            
            var all_items =  items.Select(i=>new GetItems{

                 ItemID= i.ItemID,
                     ItemName= i.ItemName,
                    ItemDescription=i.ItemDescription,
                    ItemPrice=i.ItemPrice,
                    RestaurantName= i.restaurant.RestaurantName,
                    RestaurantID= i.RestaurantID,
                    ImageUrl=$"{baseurl}{i.imageUrl}"
            }).ToList();

            return Ok(all_items);
        }
        else{
            return StatusCode(404,"No items Found");
        }
    }
        
        

    }
}


/* const cloudinaryUrl = ' https://api.cloudinary.com/v1_1/dxgirg1se/image/upload';
        const uploadPreset = 'blog-image';
         async function selectAndUploadImage(quill) {
            const input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');
            input.click();

            input.onchange = async () => {
                const file = input.files[0];
                if (file) {
                    const formData = new FormData();
                    formData.append('file', file);
                    formData.append('upload_preset', uploadPreset);

                    try {
                        const response = await fetch(cloudinaryUrl, {
                            method: 'POST',
                            body: formData
                        });

                        if (response.ok) {
                            const data = await response.json();
                            const imageUrl = data.secure_url;

                            // Insert the uploaded image into Quill
                            const range = quill.getSelection();
                            quill.insertEmbed(range.index, 'image', imageUrl);
                        } else {
                            alert('Image upload failed.');
                        }
                    } catch (error) {
                        console.error('Error uploading image:', error);
                        alert('An error occurred while uploading the image.');
                    }
                }
            };
        }
          
*/
