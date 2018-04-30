using System.Collections.Generic;

namespace BaselineSolution.Framework.Response
{
    public abstract class BaseResponse<T>
    {
        private List<Message> _messages;
        private List<T> _values;

        protected BaseResponse()
        {
            
        }

        protected BaseResponse(T value)
        {
            Values.Add(value);
        }

        protected BaseResponse(ICollection<T> values)
        {
            Values.AddRange(values);
        }

        public List<Message> Messages
        {
            get { return _messages ?? (_messages = new List<Message>()); }
            set { _messages = value; }
        }

        public List<T> Values
        {
            get => _values ?? (_values = new List<T>());
            private set => _values = value;
        }
    }
}
