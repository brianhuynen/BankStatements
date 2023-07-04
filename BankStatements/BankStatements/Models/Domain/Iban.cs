using System.ComponentModel.DataAnnotations;

namespace BankStatements.Models.Domain
{
    public class Iban
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string IbanNo { get; set; }
    }
}
