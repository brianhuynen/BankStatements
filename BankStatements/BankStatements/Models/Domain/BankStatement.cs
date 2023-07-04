using System.ComponentModel.DataAnnotations;

namespace BankStatements.Models.Domain
{
    public class BankStatement
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long ReferenceId { get; set; }
        [Required]
        public string IbanNo { get; set; }
        [Required]
        public double BalanceStart { get; set; }
        [Required]
        public double Mutation { get; set; }
        public string? Description { get; set; }
        [Required]
        public double BalanceEnd { get; set; }
    }
}
