using System.Collections.Generic;
using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Models.Shared;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.User
{
    public class EditViewModel
    {
        public UserBo User { get; set; }
        public string PasswordConfirm { get; set; }
        public MultiSelectList Roles { get; set; }
        public ICollection<AccountBo> Accounts { get; set; }
        public ICollection<SystemLanguageBo> Languages { get; set; }
    }
}