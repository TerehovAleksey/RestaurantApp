using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LogInAsync([FromBody]LoginCredentialsDto loginCredentials)
        {
            // ответ при неправильных данных
            if (string.IsNullOrWhiteSpace(loginCredentials.Username) || string.IsNullOrWhiteSpace(loginCredentials.Password))
            {
                return BadRequest();
            }

            // определяем имя пользователя или email пришли
            var isEmail = loginCredentials.Username.Contains('@');

            // ищем пользователя
            var user = isEmail ? await _userManager.FindByEmailAsync(loginCredentials.Username) :
                await _userManager.FindByNameAsync(loginCredentials.Username);

            // если пользователь не найден
            if (user == null)
            {
                return BadRequest();
            }

            // проверяем пароль
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginCredentials.Password);
            if (!isValidPassword)
            {
                return BadRequest();
            }

            // получаем username
            var username = user.UserName;

            // подготавливаем клеймы токена
            var claims = new[]
            {
                // уникальный ID для токена
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256);

            // создаём токен
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // !ВАЖНО! срок действия токена
                expires: DateTime.Now.AddDays(1));

            // возвращаем успешный результат с токеном
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}