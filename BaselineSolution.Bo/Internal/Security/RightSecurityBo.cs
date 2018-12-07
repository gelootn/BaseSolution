using System;
using System.Collections.Generic;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Contracts;
using FluentValidation;

namespace BaselineSolution.Bo.Internal.Security
{
    [Serializable]
    public class RightSecurityBo : BaseBo, ITreeHierarchy<RightSecurityBo>
    {
        private ICollection<RightSecurityBo> _children;
        public string Key { get; set; }

        public int? ParentId { get; set; }
        public RightSecurityBo Parent { get; set; }

        public ICollection<RightSecurityBo> Children
        {
            get { return _children ?? (_children = new List<RightSecurityBo>()); }
            set { _children = value; }
        }
    }
}
