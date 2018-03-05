using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BaselineSolution.Framework.Utilities
{
    public static class Enums
    {
        public static bool EnumEquals(object enumObject, object value)
        {
            int intValue;
            return enumObject != null
                   && value != null
                   && enumObject.GetType().IsEnum
                   && Int32.TryParse(value.ToString(), out intValue)
                   && Convert.ToInt32(enumObject) == Convert.ToInt32(value);

        }

       
    }
}
