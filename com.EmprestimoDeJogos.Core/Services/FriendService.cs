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

        public void BorrowGame(int id, int idGame)
        {
            _friendRepository.Borrow(id, idGame);
        }

        public IEnumerable<GameEntity> Borrows(int id)
        {
            return _friendRepository.GetBorrows(id);
        }

        public FriendEntity CreateFriend(FriendEntity friend)
        {
            return _friendRepository.Add(friend);
        }

        public bool Delete(FriendEntity friendEntity)
        {
            return _friendRepository.Delete(friendEntity);
        }

        public FriendEntity GetFriend(int id)
        {
            return _friendRepository.GetFriend(id);
        }

        public IEnumerable<FriendEntity> GetFriends()
        {
            return _friendRepository.GetFriends();

        }

        public bool Update(FriendEntity friend)
        {
            return _friendRepository.Update(friend);
        }

    }
}
