using System.ComponentModel.DataAnnotations;

namespace Wherrelz_Crud.Models
{
    public class AuditModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Table { get; set; }

        [Required]
        [MaxLength(100)]
        public string Field { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
