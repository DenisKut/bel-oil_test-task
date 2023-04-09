using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using backend.Configurations;
using backend.Dtos;
using backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Serilog;

namespace backend.Controllers
{
    [Route("api/[controller]")]// api/authentication
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
      private readonly UserManager<IdentityUser> _userManager;

      private readonly IConfiguration _configuration;
      //private readonly JwtConfig _jwtConfig;

      private readonly ILogger<AuthentificationController> _logger;
      public AuthentificationController(
        UserManager<IdentityUser> userManager,
        IConfiguration configuration,
        ILogger<AuthentificationController> logger
        //JwtConfig jwtConfig
        )
      {
        _userManager = userManager;
        //_jwtConfig = jwtConfig;
        _configuration = configuration;
        _logger = logger;
      }

      [HttpPost]
      [Route("Register")]
      public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
      {
        _logger.LogInformation("| Log || Auth || register |");
        //Validate incoming request
        if(ModelState.IsValid)
        {

          //We need to ckeck if the email already exist
          var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);

          if(user_exist != null)
          {
            return BadRequest(new AuthResult()
            {
              Result = false,
              Errors = new List<string>()
              {
                "Email already exist!"
              }
            });
          }

          //Create a user
          var new_user = new IdentityUser()
          {
            Email = requestDto.Email,
            UserName = requestDto.Email
          };

          var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);

          if(is_created.Succeeded)
          {
            //Generate the token
            var token = GenerateJwtToken(new_user);
            return Ok(new AuthResult()
            {
              Result = true,
              Token = token
            });
          }

          return BadRequest(new AuthResult()
          {
            Errors = new List<string>()
            {
              "Server error"
            },
            Result = false
          });

        }

        return BadRequest();
      }

      [Route("Login")]
      [HttpPost]
      public async Task<IActionResult> Login([FromBody] UserLoginRequiestDto loginRequiest)
      {
         _logger.LogInformation("| Log || Auth || login |");
        if(ModelState.IsValid)
        {
          //Check if the user exist
          var existing_user = await _userManager.FindByEmailAsync(loginRequiest.Email);

          if(existing_user == null)
            return BadRequest(new AuthResult()
            {
              Errors = new List<string>()
              {
                "This login does not exist"
              },
              Result = false
            });

          var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequiest.Password);

          if(!isCorrect)
          {
            return BadRequest(new AuthResult()
            {
              Errors = new List<string>()
              {
                "Invalid credentials"
              },
              Result = false
            });
          }

          var jwtToken = GenerateJwtToken(existing_user);

          return Ok(new AuthResult()
          {
            Token = jwtToken,
            Result = true
          });
        }

        return BadRequest(new AuthResult()
        {
          Errors = new List<string>()
          {
            "Invalid payload"
          },
          Result = false
        });
      }

      private string GenerateJwtToken(IdentityUser user)
      {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

        // Token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
          Subject = new ClaimsIdentity(new []
          {
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
          }),
          //time of living token
          Expires = DateTime.Now.AddHours(1),
          //Verify signature
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
          // typeof SecurityToken
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
          //Mapping into string
        return jwtTokenHandler.WriteToken(token);
      }
    }

}