using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; } = string.Empty;

        [Column(TypeName = "bit")]
        public bool IsAdmin { get; set; } = false;

        [Column(TypeName = "datetime")]
        public DateTime? RegisterDate { get; set; }

        public int? RecoveryCode { get; set; }
    }
}