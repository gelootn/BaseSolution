using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Internal.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class RestrictedRightBo : RightBo
    {

        private ICollection<RestrictedRightBo> _children;

        public RestrictedRightBo()
        {


        }

        public new ICollection<RestrictedRightBo> Children
        {
            get => _children ?? (_children = new List<RestrictedRightBo>());
            set => _children = value;
        }

        public bool IsRestricted { get; set; }

    }
}
