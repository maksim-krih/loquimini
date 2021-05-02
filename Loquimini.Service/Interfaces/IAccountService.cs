using Loquimini.Model.Entities;
using Loquimini.Model.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<UserCredentials> Login(Login login);

        User GetById(Guid id);
        Task<User> GetUserByIdAsync(Guid Id);

        Task<User> GetUserByEmailAsync(string email);

        Task<IList<string>> GetUserRolesAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<User> UpdateUserAsync(User user, IEnumerable<string> roles);

        Task<User> CreateUserAsync(User user, IEnumerable<string> roles, string password);

        Task<User> ResetPasswordAsync(User user, string newPassword);

        Task<User> UpdatePasswordAsync(User user, string currentPassword, string newPassword);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<User> DeleteUserAsync(Guid id);

        Task<User> DeleteUserAsync(User user);

        Task<Role> GetRoleByIdAsync(string roleId);

        Task<Role> GetRoleByNameAsync(string roleName);

        Task<Role> DeleteRoleAsync(Role role);

        Task<Role> CreateRoleAsync(Role role, IEnumerable<string> claims);

        Task<Role> DeleteRoleAsync(string roleName);
	}
}
