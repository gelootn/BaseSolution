using System.Collections.Generic;
using System.Linq;

namespace BaselineSolution.Framework.Response
{
    public abstract class BaseResponse<T>
    {
        private List<Message> _messages;
        private IEnumerable<T> _values;

        protected BaseResponse()
        {
            
        }

        protected BaseResponse(T value)
        {
            Values = Values.Append(value);
        }

        protected BaseResponse(IEnumerable<T> values)
        {
            Values = values;
        }

        public List<Message> Messages
        {
            get { return _messages ?? (_messages = new List<Message>()); }
            set { _messages = value; }
        }

        public IEnumerable<T> Values
        {
            get => _values ?? (_values = new List<T>());
            private set => _values = value;
        }
    }
}
