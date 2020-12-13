using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.EmprestimoDeJogos.Infrastructure.Data
{
    public class FriendRepository : IFriendRepository
    {
        private LoanGameContext _context;

        public FriendRepository(LoanGameContext context)
        {
            _context = context;
        }

        public FriendEntity Add(FriendEntity friend)
        {
            _context.Add(friend);
            _context.SaveChanges();

            return friend;
        }

        public void Delete(FriendEntity friendEntity)
        {
            _context.Set<FriendEntity>().Remove(friendEntity);
            _context.SaveChanges();
        }

        public FriendEntity GetFriend(int id)
        {
            return _context.Set<FriendEntity>().SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<FriendEntity> GetFriends()
        {
            return _context.Friends;
        }

        public void Update(FriendEntity friend)
        {
            _context.Entry(friend).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
