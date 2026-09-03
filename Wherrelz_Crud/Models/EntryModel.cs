using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Wherrelz_Crud.Models
{
    public class EntryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Account { get; set; }

        [MaxLength(500)]
        [AllowNull]
        public string Narration { get; set; }

        [Required]
        [MaxLength(10)]
        [AllowNull]
        public string Currency { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }
    }
}

