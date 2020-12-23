using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace com.EmprestimoDeJogos.Infrastructure.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly LoanGameContext _context;

        public GameRepository(LoanGameContext context)
        {
            _context = context;
        }

        public GameEntity Add(GameEntity game)
        {
            _context.Add(game);
            _context.SaveChanges();

            return game;
        }

        public int Delete(GameEntity gameEntity)
        {
            var result = _context.Set<GameEntity>().Remove(gameEntity);
            _context.SaveChanges();

            return result.Entity.Id;
        }

        public GameEntity GetGame(int id)
        {
            return _context.Set<GameEntity>().SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<GameEntity> GetGames()
        {
            return _context.Games;
        }

        public GameEntity Update(GameEntity game)
        {
            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();

            return game;
        }
    }
}
