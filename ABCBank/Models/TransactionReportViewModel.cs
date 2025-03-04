namespace ABCBank.Models
{
    public class TransactionReportViewModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<TransactionModal> Transactions { get; set; }
    }
}
