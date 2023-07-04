namespace BankStatements.Models.DTO.BankStatements
{
    public class BankStatementCreateDTO
    {
        public long ReferenceId { get; set; }
        public string IbanNo { get; set; }
        public double BalanceStart { get; set; }
        public double Mutation { get; set; }
        public string? Description { get; set; }
        public double BalanceEnd { get; set; }
    }
}
