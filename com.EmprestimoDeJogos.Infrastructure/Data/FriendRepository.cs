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

        public void Borrow(int id, int idGame)
        {
            _context.Loans.Add(new LoanEntity()
            {
                IdFriend = id,
                IdGame = idGame,
                LoanDate = DateTime.Now
            });

            _context.SaveChanges();
        }

        public void Delete(FriendEntity friendEntity)
        {
            _context.Set<FriendEntity>().Remove(friendEntity);
            _context.SaveChanges();
        }

        public IEnumerable<GameEntity> GetBorrows(int id)
        {
            //RAW SQL
            //FormattableString script = @$"SELECT g.* FROM [com.EmprestimoDeJogos.LoanGameDb].dbo.Games g
            //                RIGHT JOIN [com.EmprestimoDeJogos.LoanGameDb].dbo.Loans l
            //                ON l.IdGame = g.Id
            //                WHERE l.IdFriend = {id}";

            //return _context.Games.FromSqlInterpolated<GameEntity>(script);

            var result = from g in _context.Set<GameEntity>()
                         join loan in _context.Set<LoanEntity>().Where(l => l.IdFriend == id)
                            on g.Id equals loan.IdGame
                         select g;

            return result.AsEnumerable<GameEntity>();
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
