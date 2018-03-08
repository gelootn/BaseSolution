using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserBo : BaseBo
    {

        public UserBo()
        {
            Validator = new UserBoValidator();
        }
        /// <summary>
        ///     The _roles.
        /// </summary>
        private ICollection<RoleBo> _roles;

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(BoResources), ErrorMessageResourceName = "Required")]
        public virtual string Username { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "Password")]
        public virtual string Password { get; set; }

        [Display(ResourceType = typeof(UserBoResource), Name = "PasswordConfirm")]
        public virtual string PasswordConfirm { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "Name")]
        public virtual string Name { get; set; }

        [Display(ResourceType = typeof(UserBoResource), Name = "FirstName")]
        public virtual string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "Email")]
        public virtual string Email { get; set; }

        /// <summary>
        ///     Gets or sets the login count.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "LoginCount")]
        public virtual int? LoginCount { get; set; }

        /// <summary>
        ///     Gets or sets the last login.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "LastLogin")]
        public virtual DateTime? LastLogin { get; set; }

        /// <summary>
        ///     Gets or sets the account id.
        /// </summary>
        [Display(ResourceType = typeof(UserBoResource), Name = "AccountId")]
        public virtual int AccountId { get; set; }

        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        public virtual DisplayObject Account { get; set; }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        public virtual ICollection<RoleBo> Roles
        {
            get { return _roles ?? (_roles = new List<RoleBo>()); }
            set { _roles = new List<RoleBo>(value); }
        }

        public int[] RoleIds { get; set; }
    }

   
}
