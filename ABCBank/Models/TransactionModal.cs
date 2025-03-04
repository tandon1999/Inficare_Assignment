namespace ABCBank.Models
{
    public class TransactionModal
    {
        public int Id { get; set; }  // Primary key

        // Sender details
        public string? SenderFirstName { get; set; }
        public string? SenderMiddleName { get; set; }
        public string? SenderLastName { get; set; }
        public string? SenderAddress { get; set; }
        public string? SenderCountry { get; set; }

        // Receiver details
        public string? ReceiverFirstName { get; set; }
        public string? ReceiverMiddleName { get; set; }
        public string? ReceiverLastName { get; set; }
        public string? ReceiverAddress { get; set; }
        public string? ReceiverCountry { get; set; }

        // Payment details
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal TransferAmountMYR { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal PayoutAmountNPR { get; set; } 
        public DateTime TransferDate { get; set; }
    }
}

