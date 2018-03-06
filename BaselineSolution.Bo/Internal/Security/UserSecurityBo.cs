using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BaselineSolution.Bo.Internal.Security
{
    [DebuggerDisplay("Id: {Id}, Username: {UserName}, Email: {Email}, AccountId: {Account.Id}")]
    [Serializable]
    public class UserSecurityBo : BaseBo
    {
        private ICollection<RoleSecurityBo> _roles;

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public string DefaultCulture { get; set; }


        public AccountSecurityBo Account { get; set; }
        public ICollection<RoleSecurityBo> Roles
        {
            get { return _roles ?? (_roles = new List<RoleSecurityBo>()); }
            set
            {
                _roles = value;
            }
        }

        public bool IsAdministrator()
        {
            return Roles.Any(x => x.ParentId == null);

        }

        public bool HasRight(RightSecurityBo right)
        {
            return Roles.Any(r => r.HasRight(right));
        }
    }
}
