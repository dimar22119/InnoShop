using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
