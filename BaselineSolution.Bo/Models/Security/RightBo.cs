using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Bo.Models.Security
{
    public class RightBo : BaseBo
    {
        private ICollection<RightBo> _children;

        public RightBo()
        {
            Validator = new RightBoValidator();
        }


        [Display(ResourceType = typeof(RightBoResource), Name = "ParentId")]
        public virtual int? ParentId { get; set; }
        [Display(ResourceType = typeof(RightBoResource), Name = "Key")]
        public virtual string Key { get; set; }
        public virtual DisplayObject Parent { get; set; }

        public virtual ICollection<RightBo> Children
        {
            get => _children ?? (_children = new List<RightBo>());
            set => _children = value;
        }

        public object ToJson()
        {
            if (Children == null || !Children.Any())
            {
                return new { Id, Key, ParentId };
            }
            return new
            {
                Id,
                Key,
                ParentId,
                Children = Children.Select(child => child.ToJson())
            };
        }

        public void MergeWith(RightBo rightBo)
        {
            if (!string.Equals(Key, rightBo.Key))
                throw new ArgumentException("Keys don't match!");

            foreach (var child in rightBo.Children)
            {
                var existingChild = Children.SingleOrDefault(c => string.Equals(c.Key, child.Key));
                if (existingChild != null)
                {
                    existingChild.MergeWith(child);
                }
                else
                {
                    Children.Add(child);
                }
            }
        }

    }
}
