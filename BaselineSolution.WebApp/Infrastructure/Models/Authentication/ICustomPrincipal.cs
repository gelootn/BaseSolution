using System.Collections.Generic;
using System.Security.Principal;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Infrastructure.Models.Authentication.SessionModels;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; }

        AccountSessionModel CurrentAccount { get; set; }
        AccountSessionModel MainAccount { get; }

        int? OwnerId { get; }
        string Username { get; }
        IEnumerable<string> Roles { get; }
        IEnumerable<int> RoleIds { get; }
        IEnumerable<int> AllowedRoleIds { get; }
        bool IsAdministrator { get; }
        string DefaultCulture { get; }
        bool Owns(IOwnable ownable);
    }
}