using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
    }
}
