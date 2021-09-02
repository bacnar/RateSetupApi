using RateSetup.Models.Authentication;
using RateSetup.Models.Database;
using RateSetup.Models.UserServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateSetup.Services.UserService
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);

        Task<List<User>> GetAll();

        ValueTask<User> GetById(long id);

        Task Update(User user);

        public Task Add(User user);

        public Task Delete(User user);

        Task<bool> Exists(long id);

        Task<int> Register(RegisterUserRequest user);
    }
}
