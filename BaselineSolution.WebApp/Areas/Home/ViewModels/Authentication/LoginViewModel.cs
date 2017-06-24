using System.ComponentModel.DataAnnotations;

namespace BaselineSolution.WebApp.Areas.Home.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Display(ResourceType = typeof(Resources.WebApp), Name = "Username")]
        [Required(ErrorMessageResourceType = typeof(Resources.WebApp) , ErrorMessageResourceName = "FieldRequired")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Resources.WebApp), Name = "Password")]
        [Required(ErrorMessageResourceType = typeof(Resources.WebApp), ErrorMessageResourceName = "FieldRequired")]
        public string Password { get; set; }
    }
}