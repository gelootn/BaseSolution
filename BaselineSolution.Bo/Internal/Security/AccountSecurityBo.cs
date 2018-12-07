using System;
using System.Collections.Generic;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Contracts;
using FluentValidation;

namespace BaselineSolution.Bo.Internal.Security
{
    [Serializable]
    public class AccountSecurityBo : BaseBo, ITreeHierarchy<AccountSecurityBo>
    {
        private ICollection<AccountSecurityBo> _children;
        
        public AccountSecurityBo() 
        {
        }
        
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public AccountSecurityBo Parent { get; set; }

        public ICollection<AccountSecurityBo> Children
        {
            get { return _children ?? (_children = new List<AccountSecurityBo>()); }
            set { _children = value; }
        }

        
    }
}
