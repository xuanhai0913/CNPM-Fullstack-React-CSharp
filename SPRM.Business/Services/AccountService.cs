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
        }

        public async Task<bool> RegisterAsync(User user)
        {
            // TODO: Implement password hashing and validation
            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            // TODO: Implement password verification
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null && user.Password == password) // Simple check - should use proper hashing
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
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}