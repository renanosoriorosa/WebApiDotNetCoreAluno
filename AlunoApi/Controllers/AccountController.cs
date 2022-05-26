using AlunoApi.Services;
using AlunoApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlunoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;

        public AccountController(IConfiguration configuration, IAuthenticate authentication)
        {
            _configuration = configuration;
            _authentication = authentication;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserTokenViewModel>> CreateUser([FromBody] RegisterViewModel viewModel)
        {
            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                ModelState.AddModelError("CreateUser", "As senhas não conferem");
                return BadRequest(ModelState);
            }

            var result = await _authentication.RegisterUser(viewModel.Email, viewModel.Password);

            if (result)
            {
                return Ok($"Usuário {viewModel.Email} criado com sucesso.");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserTokenViewModel>> LoginUser([FromBody] LoginViewModel viewModel)
        {
            var result = await _authentication.Authenticate(viewModel.Email, viewModel.Password);

            if (result)
            {
                return GenerateToken(viewModel);
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserTokenViewModel> GenerateToken(LoginViewModel loginViewModel)
        {
            var claims = new[]
            {
                new Claim ("email", loginViewModel.Email),
                new Claim ("meuToken", "token renan"),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            //cria o token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials:creds
                );

            return new UserTokenViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
