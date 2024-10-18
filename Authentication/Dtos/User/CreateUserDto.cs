﻿namespace Authentication.Dtos.User
{
    public class CreateUserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
