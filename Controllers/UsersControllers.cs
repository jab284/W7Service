using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;
using EFCoreExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //This appdbcontext is used to interact with our database


      //  private readonly AppDbContext _context;


      //  public UsersController(AppDbContext context)
       // {
      //      _context = context;
      //  }


      /*
        We do not want to have the context object (database object) inside the controller.
        Instead, we will be using a service class to manage all of our database interaction, 
        and anything else we need to do.
            - for example, converting objects from DTO to entities and vice versa
            - input validation / data validation can be done

        The main thing you need tod o, is first create the interface and the classes for the services you will use.
        Then in the 


      */


      private readonly IUserService _userService;
      public UsersController(IUserService userService)
      {
        _userService = userService;
      }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
           var users = _userService.GetAllUsers(); 
           return Ok(users);
        }



        [HttpGet("{UserId}")]  //d
        public ActionResult<UserDTO> GetUserById(int UserId)
        {
            var user = _userService.GetUserById(UserId);
            return user;
        }



        [HttpPost] //d
        public ActionResult<UserDTO> PostUser(UserDTO userDto)
        {
            var user = _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { UserId = user.UserId }, userDto);
        }



        [HttpPut("{UserId}")]   // d
        public ActionResult<UserDTO> UpdateUser(int UserId, UserDTO UpdatedUser)
        {
            _userService.UpdateUser(UserId, UpdatedUser);
            return Ok(UpdatedUser);
        }



         [HttpDelete("{UserId}")]   //d
        public IActionResult DeleteUser(int UserId)
        {
            _userService.DeleteUser(UserId);
            return Ok();
        }

    }
}