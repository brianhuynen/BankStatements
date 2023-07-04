using BankStatements.Models.Domain;

namespace BankStatements.Services
{
    public interface IIbanService
    {
        public Task<IEnumerable<Iban>> GetAllIbanAsync();
    }
}
