using AutoMapper;
using Loquimini.Mapping;
using Loquimini.Model.User;

namespace Loquimini.ModelDTO
{
    public class LoginDTO : IProfileBase
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public IProfileExpression Configure(IProfileExpression config)
        {
            config.CreateMap<LoginDTO, Login>();

            return config;
        }
    }
}
