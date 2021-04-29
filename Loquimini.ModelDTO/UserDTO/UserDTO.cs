using System;

namespace Loquimini.ModelDTO.UserDTO
{
    public class UserDTO //: IProfileBase
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }
        
        //public IProfileExpression Configure(IProfileExpression config)
        //{
        //    config.CreateMap<User, UserDTO>()
        //        .ForMember(x => x.PhoneNumber, cfg => cfg.MapFrom(x => x.UserNameDecrypted));

        //    return config;
        //}
    }
}
