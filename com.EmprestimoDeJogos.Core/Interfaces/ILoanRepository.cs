using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Core.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<LoanEntity> GetLoans();

        LoanEntity Add(LoanEntity loan);
    }
}
