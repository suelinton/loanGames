using System;

namespace com.EmprestimoDeJogos.Core.Entities
{
    public class LoanEntity : BaseEntity
    {
        public int IdGame { get; set; }
        public int IdFriend { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
