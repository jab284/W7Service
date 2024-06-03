using EFCoreExample.DTOs;
using EFCoreExample.Models;

namespace EFCoreExample.Services
{

        /*We use the interface because the Dependency COntainer prefers having the most generic version of the dependency to work with.
        It will look
        */
    public interface IUserService
    {
        IEnumerable<UserDTO>GetAllUsers();

        UserDTO GetUserById(int UserId);

        User CreateUser(UserDTO UserDto);

        void UpdateUser(int UserId, UserDTO UpdatedUser);

        void DeleteUser(int UserId);

    }
}