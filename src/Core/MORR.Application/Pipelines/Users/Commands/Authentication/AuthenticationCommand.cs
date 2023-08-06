using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MORR.Application.Common.Constants;
using MORR.Application.DTOs.AuthenticationDTOs;
using MORR.Domain.Entities;
using MORR.Domain.Repositories.Query;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MORR.Application.Pipelines.Users.Commands.Authentication
{
    public class AuthenticationCommand : IRequest<UserAuthenticationResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, UserAuthenticationResponseDto>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationCommandHandler(IUserQueryRepository userQueryRepository, IConfiguration configuration)
        {
            this._userQueryRepository = userQueryRepository;
            this._configuration = configuration;
        }

        public async Task<UserAuthenticationResponseDto> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = (await _userQueryRepository
                          .Query(x=>x.UserName == request.UserName && x.IsActive == true))
                          .FirstOrDefault();

                if (user is null)
                {
                    return UserAuthenticationResponseDto.NotSuccess(ResponseConstants.USER_DOES_NOT_EXIST_RESPONSE);
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return UserAuthenticationResponseDto.NotSuccess(ResponseConstants.PASSWORD_INCORRECT_RESPONSE);
                }

                var userAthunticationResponse = await ConfigureJwtToken(user, cancellationToken);

                if (userAthunticationResponse.IsLoginSuccess)
                {
                    return userAthunticationResponse;
                }
                else
                {
                    return UserAuthenticationResponseDto.NotSuccess(ResponseConstants.COMMON_EXCEPTION_RESPONSE);
                }
            }
            catch (Exception ex)
            {

                return UserAuthenticationResponseDto.NotSuccess(ResponseConstants.COMMON_EXCEPTION_RESPONSE);
            }
        }

        private async Task<UserAuthenticationResponseDto> ConfigureJwtToken(User user,  CancellationToken cancellationToken)
        {
            var key = _configuration["Tokens:Key"];
            var issuer = _configuration["Tokens:Issuer"];

            string role = user.UserRoles.FirstOrDefault()!.Role.Name;

            var now = DateTime.UtcNow;
            DateTime nowDate = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim("firstName",string.IsNullOrEmpty(user.FirstName)? "": user.FirstName),
                        new Claim("lastName", string.IsNullOrEmpty(user.LastName) ? "" : user.LastName),
                        new Claim("role",role),
                        new Claim(JwtRegisteredClaimNames.Aud,"webapp"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };



            var token = new JwtSecurityToken
            (
                issuer: issuer,
                claims: claims,
                expires: nowDate.AddHours(8),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                            .WriteToken(token);

            var roles = user.UserRoles
                            .Select(t => t.Role)
                            .Select(t => t.Name)
                            .ToList();

          

            return UserAuthenticationResponseDto.Success
                (
                    tokenString,
                    $"{user.FirstName} {user.LastName}",
                    user.Id,   
                    roles
                );
        }
    }
}
