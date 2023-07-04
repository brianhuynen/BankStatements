using BankStatements.Models.Domain;

namespace BankStatements.Services
{
    public interface IBankStatementService
    {
        public Task<IEnumerable<BankStatement>> GetAllStatementsAsync();
        public Task AddStatementAsync(BankStatement statement);
    }
}
