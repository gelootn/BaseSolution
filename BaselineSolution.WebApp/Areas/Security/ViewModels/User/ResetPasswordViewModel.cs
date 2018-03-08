using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        public ResetPasswordViewModel(UserBo user)
        {
            User = user;
            Password = new UserSetPasswordBo
            {
                Id= user.Id
            };
        }
        public UserBo User { get; set; }
        public UserSetPasswordBo Password { get; private set; }
    }

}