using SPRM.Data.Entities;
using SPRM.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPRM.Business.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(User user);
        Task<User?> LoginAsync(string username, string password);
        bool Register(User user);
        User? Login(string username, string password);
        
        // Additional async methods for MVC controllers
        Task<bool> RegisterAsync(UserDto userDto);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
