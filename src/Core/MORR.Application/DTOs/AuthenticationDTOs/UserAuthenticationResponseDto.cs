namespace MORR.Application.DTOs.AuthenticationDTOs
{
    public class UserAuthenticationResponseDto
    {
        internal UserAuthenticationResponseDto(bool isLoginSuccess, string token, string displayName, int userId, string message, List<string> roles)
        {
            IsLoginSuccess = isLoginSuccess;
            Token = token;
            DisplayName = displayName;
            UserId = userId;
            Message = message;  
            Roles = roles;
        }

        public bool IsLoginSuccess { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public List<string> Roles { get; set; }


        public static UserAuthenticationResponseDto NotSuccess(string errorMessage)
        {
            return new UserAuthenticationResponseDto(false, string.Empty, string.Empty, 0, errorMessage, new List<string>());
        }

        public static UserAuthenticationResponseDto Success(string token, string displayName, int userId,List<string> roles)
        {
            return new UserAuthenticationResponseDto(true, token, displayName, userId, "User login is successfully.", roles);
        }
    }
}
