using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Domain.Security
{
    /// <summary>
    ///     The user.
    /// </summary>
    [DebuggerDisplay("Id: {Id}, Username: {Username}, Email: {Email}, AccountId: {AccountId}")]
    public class User : Entity
    {
        /// <summary>
        ///     The _roles.
        /// </summary>
        private ICollection<Role> _roles;

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
        public virtual Account Account { get; set; }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = new List<Role>(value); }
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Username: {Username}, Email: {Email}, AccountId: {AccountId}";
        }


    }
}
