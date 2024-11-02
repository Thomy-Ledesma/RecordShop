using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AddAdminRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
