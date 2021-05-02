using AutoMapper;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;

namespace Loquimini.ModelDTO.UserDTO
{
    public class RoleDTO : IProfileBase
    {
        public Guid Id { get; set; }
     
        public string Name { get; set; }
        
        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<Role, RoleDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore());

            return config;
        }
    }
}
