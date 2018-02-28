using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.Bo.Filters.Security
{
    public class UserFilter
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
