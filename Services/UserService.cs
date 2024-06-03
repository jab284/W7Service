using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{
    public class UserService : IUserService
    {

            /* The service class implements the interface.
            This means that all the declared methods in the interface must have some kind of implementation inside the service.
            
            We are also using dependency injection here to have access to the database context
            
            Most of the core methods inside the service will just be moving the things that were happening inside the controller endpoints, 
            into the service methods.
            
            Not much needs to change, most of the changes will come from needing to add in additional validations and checks to improve the stability of your application.
            
            Thing like validating if the new data is in the right forms, or iof an id is provided for a user that doesn't exist, 
            figure out someway to work with null checking in your service layer */

        private readonly AppDbContext _context;

        public UserService(AppDbContext context){
            _context = context;
        }

        public User CreateUser(UserDTO UserDto)
        {
            if (ValidateNewUser(UserDto))
            {
                var user = new User
                {
                    Name = UserDto.Name
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                return user;
            }  else
            {
                throw new Exception("Invalid User");
            }
        }

        private bool ValidateNewUser(UserDTO UserDto)
        {
            return !string.IsNullOrWhiteSpace(UserDto.Name);
            /*
            if(UserDto.Name.Trim().Length ==0){
                return false;
            }else return true;  */
        }

        public void DeleteUser(int UserId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == UserId);
             if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
           var users = _context.Users
                 .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                }).ToList();
                return users;
        }

        public UserDTO GetUserById(int UserId)
        {
           var user = _context.Users.Find(UserId);

             if (user != null)
            {
                var userDto = new UserDTO
                {
                    Name = user.Name,
                    UserId = user.UserId
                };

                return userDto;
            }
            else
            {
                throw new Exception("User not found");
            }


            /*
           var userDto = new UserDTO
            {
                Name = user.Name,
               UserId = user.UserId
            };
            return userDto;   */
        }

        public void UpdateUser(int UserId, UserDTO UpdatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == UserId);
              if (user != null)
            {
                user.Name = UpdatedUser.Name;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }

            /*
            user.Name = UpdatedUser.Name;

            _context.Users.Update(user);
            _context.SaveChanges();    */
        }
    }

}