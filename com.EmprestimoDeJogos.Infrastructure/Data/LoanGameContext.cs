using com.EmprestimoDeJogos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace com.EmprestimoDeJogos.Infrastructure
{
    public class LoanGameContext : DbContext
    {
        public LoanGameContext(DbContextOptions<LoanGameContext> options) : base(options)
        {

        }

        public DbSet<FriendEntity> Friends { get; set; }
        public DbSet<GameEntity> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
