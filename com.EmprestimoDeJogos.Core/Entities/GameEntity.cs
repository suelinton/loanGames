using System;

namespace com.EmprestimoDeJogos.Core.Entities
{
    public class GameEntity : BaseEntity
    {
        public string Name { get; set; }

        public int FriendId { get; set; }
        public FriendEntity Friend { get; set; }
    }
}
