using System.Collections.Generic;
using BaselineSolution.Bo.Internal;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserCommitBo : BaseBo
    {
        /// <summary>
        ///     The _roles.
        /// </summary>
        private ICollection<int> _roles;

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }
        public virtual string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        public virtual string Email { get; set; }



        /// <summary>
        ///     Gets or sets the account id.
        /// </summary>
        public virtual int AccountId { get; set; }


        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        public virtual ICollection<int> Roles
        {
            get { return _roles ?? (_roles = new List<int>()); }
            set { _roles = new List<int>(value); }
        }
    }
}
