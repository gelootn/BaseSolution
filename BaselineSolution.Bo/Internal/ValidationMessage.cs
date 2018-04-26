using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaselineSolution.Bo.Internal
{
    [Serializable]
    public class ValidationMessage
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public string AttemptedValue { get; set; }
    }
}
