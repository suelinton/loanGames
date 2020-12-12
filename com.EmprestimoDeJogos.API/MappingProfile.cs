using AutoMapper;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.EmprestimoDeJogos.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameEntity, GameDto>();
        }
    }
}
