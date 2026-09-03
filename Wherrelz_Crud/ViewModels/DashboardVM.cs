namespace Wherrelz_Crud.ViewModels
{
    public class DashboardVM
    {
        public int TotalEntries { get; set; }

        public decimal TotalCredit { get; set; }

        public decimal TotalDebit { get; set; }

        public decimal Balance { get; set; }
        public decimal HighestCredit { get; set; }
        public decimal HighestDebit { get; set; }
    }
}
