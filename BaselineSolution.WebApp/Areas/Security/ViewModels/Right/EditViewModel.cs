using System.Collections.Generic;
using System.Web.Mvc;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.Right
{
    public class EditViewModel
    {
        public RightBo RightBo { get; set; }

        public SelectList Rights { get; set; }
    }

}