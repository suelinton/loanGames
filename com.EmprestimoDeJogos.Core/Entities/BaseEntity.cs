using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.Core.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }
}
