using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Infrastructure.Data
{
    public class FriendRepository : IFriendRepository
    {
        private readonly LoanGameContext _context;

        public FriendRepository(LoanGameContext context)
        {
            _context = context;
        }

        public IEnumerable<FriendEntity> GetFriends()
        {
            return _context.Friends;
        }
    }
}
