using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Infrastructure.Data
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanGameContext _context;

        public LoanRepository(LoanGameContext context)
        {
            _context = context;
        }

        public LoanEntity Add(LoanEntity loan)
        {
            _context.Add(loan);
            _context.SaveChanges();

            return loan;
        }

        public IEnumerable<LoanEntity> GetLoans()
        {
            return _context.Loans;
        }
    }
}
