using Loquimini.Common.Exceptions;
using Loquimini.Manager.Interfaces;
using Loquimini.Model.Entities;
using Loquimini.Model.User;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.Service
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager; 
        private readonly IConfiguration _config;
        private readonly ITokenManager _tokenManager;


        public AccountService(
            IDatabaseManager databaseManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration config,
            ITokenManager tokenManager
            ) : base(databaseManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _tokenManager = tokenManager;
        }

        public async Task<UserCredentials> Login(Login login)
        {
            var user = await _databaseManager.UserRepository
                             .Get(x => x.Email == login.Email)
                             .Include(u => u.UserRoles)
                                .ThenInclude(up => up.Role)
                             .FirstOrDefaultAsync();

            if (user != null && !user.IsDeleted)
            {
                if (!string.IsNullOrEmpty(login.Password) && await CheckPasswordAsync(user, login.Password))
                {
                    return GetUserCredentials(user);
                }
                else
                {
                    throw new ConflictStatusError("IncorrectCredentials");
                }
            }
            else
            {
                throw new ConflictStatusError("UserDoesntExist");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            return await _userManager.FindByIdAsync(Id.ToString());
        }

        public User GetById(Guid id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await UpdateUserAsync(user, null);
        }

        public async Task<User> UpdateUserAsync(User user, IEnumerable<string> roles)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }


            if (roles != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var rolesToRemove = userRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();

                if (rolesToRemove.Any())
                {
                    result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.FirstOrDefault().Description);
                    }
                }

                if (rolesToAdd.Any())
                {
                    result = await _userManager.AddToRolesAsync(user, rolesToAdd);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.FirstOrDefault().Description);
                    }
                }
            }

            return user;
        }

        public async Task<User> ResetPasswordAsync(User user, string newPassword)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }

            return user;
        }

        public async Task<User> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }

            user = _databaseManager.UserRepository.GetById(user.Id);

            return user;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
			if (!await _userManager.CheckPasswordAsync(user, password))
            {
				if (!_userManager.SupportsUserLockout)
				{
					await _userManager.AccessFailedAsync(user);
				}
			
                return false;
			}

			return true;
        }

        public async Task<User> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return user;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
            
            return user;
        }

        public async Task<Role> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<Role> CreateRoleAsync(Role role, IEnumerable<string> claims)
        {
            if (claims == null)
            {
                claims = new string[] { };
            }

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(e => e.Description).First());
            }


            role = await _roleManager.FindByNameAsync(role.Name);

            foreach (string claim in claims.Distinct())
            {
                if (!result.Succeeded)
                {
                    await DeleteRoleAsync(role);
                    throw new Exception(result.Errors.Select(e => e.Description).First());
                }
            }

            return role;
        }

        public async Task<Role> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                return await DeleteRoleAsync(role);
            }

            return role;
        }

        public async Task<Role> DeleteRoleAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(e => e.Description).First());
            }

            return role;
        }

        public async Task<User> CreateUserAsync(User user, IEnumerable<string> roles, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(e => e.Description).FirstOrDefault());
            }

            try
            {
                result = await _userManager.AddToRolesAsync(user, roles.Distinct());
            }
            catch (Exception e)
            {
                await DeleteUserAsync(user);
                throw;
            }

            if (!result.Succeeded)
            {
                await DeleteUserAsync(user);
                throw new Exception(result.Errors.Select(e => e.Description).FirstOrDefault());
            }

            return user;
        }

        private UserCredentials GetUserCredentials(User user)
        {
            var accessToken = new AccessToken(_tokenManager.CreateToken(user), int.Parse(_config["Authorization:TokenExpirationIn"]));

            return new UserCredentials()
            {
                AccessToken = accessToken,
                CredentialsInfo = new CredentialsInfo()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Id = user.Id,
                    Roles = user.UserRoles.Select(x => x.Role.Name)
                }
            };
        }
    }
}
