using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselineSolution.Bo.Validators.Security
{
    internal static class PasswordValidator
    {
        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            if (password.Length < 6)
                return false;

            return true;
        }
    }
}
