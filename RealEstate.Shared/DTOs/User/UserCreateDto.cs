namespace RealEstate.Shared.DTOs.User
{
    public class UserCreateDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
