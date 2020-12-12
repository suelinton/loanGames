using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using System.Collections.Generic;

namespace com.EmprestimoDeJogos.Core.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public FriendEntity CreateFriend(FriendEntity friend)
        {
            return _friendRepository.Add(friend);
        }

        public IEnumerable<FriendEntity> GetFriends()
        {
            return _friendRepository.GetFriends();
        }
        
    }
}
