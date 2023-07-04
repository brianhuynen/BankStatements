using BankStatements.Models;
using BankStatements.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Services
{
    public class IbanService : IIbanService
    {
        private readonly BankStatementContext _context;

        public IbanService(BankStatementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Iban>> GetAllIbanAsync()
        {
            return await _context.Ibans.ToListAsync();
        }
    }
}
