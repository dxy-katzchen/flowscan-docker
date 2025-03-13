using API.Data;
using API.Models.DTOs.Requests.Auth;
using API.Models.Response;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly StoreContext _context;

        public AuthController(StoreContext storeContext)
        {
            _context = storeContext;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="authRequestDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessResponse<string>), 200)]

        public async Task<ActionResult<SuccessResponse<string>>> Login([FromBody] AuthRequestDto authRequestDto)
        {
            var candidates = await _context.Credentials.ToListAsync();

            var user = candidates.FirstOrDefault(u =>
       BCrypt.Net.BCrypt.Verify(authRequestDto.CredentialCode, u.InvitationCodeHash)
   );
            if (user == null)
            {
                return BadRequest(new ErrorResponse<string>("Credential code invalid"));
            }

            var token = GenerateToken(user);
            return Ok(new SuccessResponse<string>($"Bearer {token}", "Token generated successfully"));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessResponse<string>), 200)]
        public async Task<ActionResult<SuccessResponse<string>>> Register([FromBody] AuthRequestDto authRequestDto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(authRequestDto.CredentialCode);
            try
            {
                await ExamineDuplicatedVerificationCode(authRequestDto.CredentialCode);
            }
            catch (System.Exception e)
            {
                return BadRequest(new ErrorResponse<string>(e.Message));
            }
            var newUser = new Credential
            {
                InvitationCodeHash = hashedPassword
            };
            _context.Credentials.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(new SuccessResponse<string>(hashedPassword));
        }

        private async Task ExamineDuplicatedVerificationCode(string verificationCode)
        {
            var candidates = _context.Credentials.ToList();
            var user = candidates.FirstOrDefault(u =>
                BCrypt.Net.BCrypt.Verify(verificationCode, u.InvitationCodeHash)
            );
            if (user != null)
            {
                throw new System.Exception("Credential code already exists");
            }
        }



        private string GenerateToken(Credential user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("bitflow_auckland_eye_bitflow_auckland_eye_2024");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}