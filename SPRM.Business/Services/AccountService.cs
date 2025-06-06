using SPRM.Data.Entities;
using SPRM.Data.Interfaces;
using SPRM.Business.Interfaces;
using SPRM.Business.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPRM.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }        public async Task<bool> RegisterAsync(User user)
        {
            // Hash password before saving
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            
            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        // Keep synchronous methods for interface compatibility
        public bool Register(User user)
        {
            return RegisterAsync(user).Result;
        }        public User? Login(string username, string password)
        {
            return LoginAsync(username, password).Result;
        }

        // Additional async methods for DTO operations
        public async Task<bool> RegisterAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await RegisterAsync(user);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
            return true;
        }        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> RegisterAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var success = await RegisterAsync(user);
            if (success)
            {
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}