using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? ClientUri { get; set; }
    }
}
