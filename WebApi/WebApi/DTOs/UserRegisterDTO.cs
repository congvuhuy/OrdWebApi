namespace WebApi.DTOs
{
    public class UserRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Description { get; set; }
    }
}
