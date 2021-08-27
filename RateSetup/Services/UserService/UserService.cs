using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateSetup.Helpers;
using RateSetup.Models;
using RateSetup.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RateSetup.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly EFContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserService(EFContext context, IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public Task<List<User>> GetAll()
        {
            return _context.User.ToListAsync();
        }

        public ValueTask<User> GetById(long id)
        {
            return _context.User.FindAsync(id);
        }

        public Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }

        public Task Add(User user)
        {
            _context.User.Add(user);

            return _context.SaveChangesAsync();
        }

        public Task Delete(User user)
        {
            _context.User.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<bool> Exists(long id)
        {
            return _context.User.AnyAsync(e => e.Id == id);
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
