using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication.SessionModels
{
    [Serializable]
    public class AccountSessionModel : ITreeHierarchy<AccountSessionModel>, IIdentifiable
    {
        private ICollection<AccountCultureSessionModel> _accountCultures;
        private ICollection<AccountSessionModel> _children;
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the parent id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent
        /// </summary>
        public AccountSessionModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the children
        /// </summary>
        public ICollection<AccountSessionModel> Children
        {
            get { return _children ?? (_children = new List<AccountSessionModel>()); }
            set { _children = value; }
        }

        public ICollection<AccountCultureSessionModel> AccountCultures
        {
            get { return _accountCultures ?? (_accountCultures = new List<AccountCultureSessionModel>()); }
            set { _accountCultures = value; }
        }



        public int NumberOfParents
        {
            get
            {
                return Parent == null ? 0 : 1 + Parent.NumberOfParents;
            }
        }

        public AccountSessionModel(AccountSessionModel parent, AccountSecurityBo account)
        {
            Id = account.Id;
            Name = account.Name;
            Parent = parent;
            Children = new HashSet<AccountSessionModel>(account.Children.Select(c => new AccountSessionModel(this, c)));

        }

        protected bool Equals(AccountSessionModel other)
        {
            return Id == other.Id && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((AccountSessionModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}