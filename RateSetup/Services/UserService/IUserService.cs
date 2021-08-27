using RateSetup.Models;
using RateSetup.Models.Authentication;
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
    }
}
