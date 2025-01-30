using System.Runtime.CompilerServices;
using FakeRestuarantAPI.Data;
using FakeRestuarantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeRestuarantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly ILogger<RestaurantController> _logger;
        public readonly AppDbContext _context;

       public UserController(ILogger<RestaurantController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult> registeruserAsync(UserDTO userDTO)
    {
          string customerID="";
    bool isUnique = false;
    var userexists= await _context.User.AnyAsync(u=>u.UserEmail== userDTO.UserEmail);
    // Loop until a unique ID is generated
    if(!userexists)
    {
    while (!isUnique)
    {
        // Generate a custom random string
        customerID = Guid.NewGuid().ToString(); // Use this for a GUID-based string

            // Check if this string is already in use
            isUnique = !await _context.User.AnyAsync(r => r.Usercode == customerID);
    }

    User user = new User{
        UserEmail= userDTO.UserEmail,
        Password= userDTO.Password,
        Usercode= customerID
    };


    await _context.User.AddAsync(user);
    await _context.SaveChangesAsync();
    return StatusCode(201, user);
    }
    else
    {
        return StatusCode(401, new{message="user already exists"});
    }

    }

    [HttpGet("getusercode")]
    public async Task<ActionResult> getusercode([FromQuery] string UserEmail, string Password){
        var user = await _context.User.FirstOrDefaultAsync(u=>u.UserEmail == UserEmail);
        if(user!= null && Password == user.Password)
        {
            return Ok(new {usercode= user.Usercode});
        }
        else{
            return StatusCode(404,new {message="Invalid Details"});
        }
    }
    [HttpGet]
    public async Task<ActionResult> getusers()
    {
        var userexists=  await _context.User.AnyAsync();
        if(userexists) 
        {
        var users= await _context.User.Select(u=>new User{
            Usercode=u.Usercode,
            UserEmail= u.UserEmail,
            Password=u.Password
        }).ToListAsync();

        return Ok(users);
        }
        else
        {
            return StatusCode(404,"no users");
        }
    }

    [HttpDelete("{apikey}")]

    public async Task<ActionResult> DeleteUser(string apikey)
    {
        var userdata= await _context.User.FirstOrDefaultAsync(u=>u.Usercode == apikey);
        if(userdata != null)
        {
            _context.User.Remove(userdata);
            await _context.SaveChangesAsync();
            return StatusCode(200,new {message="User Data Deleted"});
        }
        else{
            return StatusCode(404, new{message="No User Data Found"});
        }
    }

    [HttpPut("{apikey}")]
    public async Task<ActionResult> UpdateUser(string apikey,  [FromBody] string NewPassword)
    {
        var userexists = await _context.User.FirstOrDefaultAsync(u=>u.Usercode == apikey);
        if(userexists!= null)
        {
            
            userexists.Password = NewPassword;

        // User user= new User{
        //     Usercode= apikey,
        //     UserEmail= userDTO.UserEmail,
        //     Password=userDTO.Password
        // };
        //  _context.User.Update(userexists);
         await _context.SaveChangesAsync();
         return StatusCode(201, userexists);
        }
        else
        {
             return StatusCode(404, new{message="No User Data Found"});
        }
        

    }
    }
}
