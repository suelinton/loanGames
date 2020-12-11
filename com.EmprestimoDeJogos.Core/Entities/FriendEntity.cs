using System;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Entities
{
    public class FriendEntity : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<GameEntity> GameEntities { get; set; }
    }
}
