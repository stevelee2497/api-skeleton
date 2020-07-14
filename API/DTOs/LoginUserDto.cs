using System;

namespace API.DTOs
{
    public class LoginUserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserResponseDto
    {
        public UserDto Profile { get; set; }

        public string Token { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
