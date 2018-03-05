using System.Collections.Generic;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.Account
{
    public class EditViewModel
    {
        public AccountBo AccountBo { get; set; }
        public IEnumerable<DisplayObject> ParentAccounts { get; set; }
    }
}
