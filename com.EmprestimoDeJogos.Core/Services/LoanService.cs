using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository gameRepository)
        {
            _loanRepository = gameRepository;
        }

        public LoanEntity CreateLoan(LoanEntity loan)
        {
            return _loanRepository.Add(loan);
        }

        public IEnumerable<LoanEntity> GetLoans()
        {
            return _loanRepository.GetLoans();
            
        }
    }
}
