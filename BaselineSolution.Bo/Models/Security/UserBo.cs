using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserBo : BaseBo
    {
        /// <summary>
        ///     The _roles.
        /// </summary>
        private ICollection<DisplayObject> _roles;

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        public virtual string Password { get; set; }

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
        ///     Gets or sets the login count.
        /// </summary>
        public virtual int? LoginCount { get; set; }

        /// <summary>
        ///     Gets or sets the last login.
        /// </summary>
        public virtual DateTime? LastLogin { get; set; }

        /// <summary>
        ///     Gets or sets the account id.
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        public virtual DisplayObject Account { get; set; }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        public virtual ICollection<DisplayObject> Roles
        {
            get { return _roles ?? (_roles = new List<DisplayObject>()); }
            set { _roles = new List<DisplayObject>(value); }
        }
    }

    public class MapToAttribute : Attribute
    {
        public MapToAttribute(Func<object, object> func)
        {
            throw new NotImplementedException();
        }
    }
}
