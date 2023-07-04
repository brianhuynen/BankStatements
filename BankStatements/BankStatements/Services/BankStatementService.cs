using BankStatements.Exceptions;
using BankStatements.Models;
using BankStatements.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Services
{
    public class BankStatementService : IBankStatementService
    {
        private readonly BankStatementContext _context;

        public BankStatementService(BankStatementContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all BankStatements stored in the DB
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BankStatement>> GetAllStatementsAsync()
        {
            return await _context.BankStatements.ToListAsync();
        }
        /// <summary>
        /// Adds a BankStatement to the DB
        /// </summary>
        /// <param name="bankStatement"></param>
        /// <returns></returns>
        /// <exception cref="InvalidIbanException"></exception>
        /// <exception cref="DuplicateReferenceIdException"></exception>
        /// <exception cref="MutationMismatchException"></exception>
        public async Task AddStatementAsync(BankStatement bankStatement)
        {
            if (await _context.Ibans.FirstOrDefaultAsync(iban => iban.IbanNo == bankStatement.IbanNo) == null)
                throw new InvalidIbanException();
            if (await _context.BankStatements.FirstOrDefaultAsync(bs => bs.ReferenceId == bankStatement.ReferenceId) != null)
                throw new DuplicateReferenceIdException("Duplicate transaction reference");
            if (bankStatement.BalanceStart + bankStatement.Mutation != bankStatement.BalanceEnd)
                throw new MutationMismatchException("End Balance does not match mutation");

            _context.BankStatements.Add(bankStatement);
            await _context.SaveChangesAsync();
        }
    }
}
