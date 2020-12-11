﻿using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<GameEntity> GetGames()
        {
            return _context.Games;
        }
    }
}
