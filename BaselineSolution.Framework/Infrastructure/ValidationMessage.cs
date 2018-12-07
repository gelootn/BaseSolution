using System;

namespace BaselineSolution.Framework.Infrastructure
{
    [Serializable]
    public class ValidationMessage
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public string AttemptedValue { get; set; }
    }
}
