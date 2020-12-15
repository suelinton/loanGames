using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.Core.Services
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
