using System.ComponentModel.DataAnnotations;

namespace Wherrelz_Crud.ViewModels
{
    public class EntryVM
    {
        public int Id { get; set; }
       
        public string? Account { get; set; }

        public string? Narration { get; set; }

        public string? Currency { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }
    }
}
