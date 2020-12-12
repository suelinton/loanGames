using com.EmprestimoDeJogos.Core.Entities;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<LoanEntity> GetLoans();
        LoanEntity CreateLoan(LoanEntity loan);
    }
}
