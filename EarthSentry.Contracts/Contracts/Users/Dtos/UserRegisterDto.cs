﻿namespace EarthSentry.Contracts.Contracts.Users.Dtos
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; } = "";
        public string Password { get; set; }
    }
}
