using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Models.Authentication.SessionModels;

namespace BaselineSolution.WebApp.Components.Models.Authentication
{
    [Serializable]
    public class AuthenticatedCustomPrincipal : ICustomPrincipal
    {
        private AccountSessionModel _currentAccount;
        public int Id { get; private set; }

        public AccountSessionModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _currentAccount = value;
                OwnerId = _currentAccount.Id;
            }
        }

        public AccountSessionModel MainAccount { get; private set; }
        public int? OwnerId { get; private set; }
        public string Username { get; private set; }
        public IEnumerable<string> Roles { get; private set; }
        public IEnumerable<int> RoleIds { get; private set; }
        public IEnumerable<int> AllowedRoleIds { get; private set; }
        public bool IsAdministrator { get; private set; }

        public string DefaultCulture { get; private set; }

        public bool Owns(IOwnable ownable)
        {
            return ownable.OwnerId == OwnerId;
        }

        public AuthenticatedCustomPrincipal(UserSecurityBo user, List<SystemLanguageBo> languages)
        {
            Id = user.Id;
            Username = user.UserName;
            DefaultCulture = user.DefaultCulture;
            MainAccount = new AccountSessionModel(null, user.Account);
            CurrentAccount = MainAccount;

            CurrentAccount.AccountCultures =
                languages.Select(x => new AccountCultureSessionModel(x)).ToList();

            var roles = user.Roles.ToList();
            Roles = roles.Select(r => r.Name).ToArray();
            RoleIds = roles.Select(r => r.Id).ToArray();
            AllowedRoleIds = roles.SelectMany(s => s.Flatten()).Select(s => s.Id).ToArray();
            OwnerId = CurrentAccount.Id;
            Identity = new GenericIdentity(Username);
            IsAdministrator = user.IsAdministrator();



        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public IIdentity Identity { get; private set; }

     
    }
}