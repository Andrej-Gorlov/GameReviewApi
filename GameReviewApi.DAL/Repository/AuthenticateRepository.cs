﻿using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Authenticate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameReviewApi.DAL.Repository
{
    public class AuthenticateRepository: IAuthenticateRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public AuthenticateRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<bool> Register (Register entity)
        {
            var userExists = await _db.User.FirstOrDefaultAsync(x => x.UserName == entity.UserName && x.Email == entity.Email);
            if (userExists != null) 
            {
                return false;
            }
            User user = new()
            {
                UserName = entity.UserName,
                Email = entity.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(entity.Password),
                Role=Role.User
            };
            _db.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RegisterAdmin(Register entity)
        {
            var userExists = await _db.User.FirstOrDefaultAsync(x => x.UserName == entity.UserName && x.Email == entity.Email);
            if (userExists != null) 
            {
                return false;
            }
            User user = new()
            {
                UserName = entity.UserName,
                Email = entity.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(entity.Password),
                Role = Role.Admin
            };
            _db.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<string> Login(Login entity)
        {
            var user = await _db.User.FirstOrDefaultAsync(x => x.UserName == entity.UserName);
            if (user != null || !BCrypt.Net.BCrypt.Verify(user.Password, entity.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return "401";
        }
    }
}
