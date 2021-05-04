using AutoMapper;
using Loquimini.Mapping;
using Loquimini.Model.Entities;
using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.UserDTO
{
    public class UserDTO: IProfileBase
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<User, UserDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, cfg => cfg.Ignore())
                .ForMember(x => x.UserName, cfg => cfg.MapFrom(x => x.Email));

            return config;
        }
    }
}
