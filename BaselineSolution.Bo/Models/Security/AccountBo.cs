using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Validators.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class AccountBo : BaseBo
    {
        public AccountBo()
        {
            Validator = new AccountBoValidator();
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DisplayObject Parent { get; set; }

    }
}
