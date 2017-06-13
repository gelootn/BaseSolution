using System.Collections.Generic;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.DAL.Domain.Security
{
    public class Account : Entity, ITreeHierarchy<Account>
    {
        public virtual int? ParentId { get; set; }
        public virtual Account Parent { get; set; }
        public virtual ICollection<Account> Children { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the Users
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
