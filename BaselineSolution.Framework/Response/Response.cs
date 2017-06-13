using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BaselineSolution.Framework.Response
{
    /// <summary>
    /// Messages is initiated in the constructor (so never null)
    /// </summary>
    public class Response<T>
    {
        private List<Message> _messages;
        private List<T> _values;

        public Response()
        {
            
        }

        public Response(T value)
        {   
            Values.Add(value);
        }

        public Response(ICollection<T> values)
        {
            Values.AddRange(values);
        }

        public List<T> Values
        {
            get { return _values ?? (_values = new List<T>()); }
            set { _values = value; }
        }

        public bool HasValue => Values.Count != 0;

        public T GetValue()
        {
            if (Values != null && Values.Count == 1)
                return Values.First();
            else
                return default(T);
        }

        public bool IsSuccess
        {
            get
            {
                return Messages.Any(c => c.Type == MessageType.Error) == false;
            }
        }
        public bool HasWarnings
        {
            get { return Messages.Any(c => c.Type == MessageType.Warning); }
        }

        public List<Message> Messages
        {
            get { return _messages ?? (_messages = new List<Message>()); }
            set { _messages = value; }
        }

        public override string ToString()
        {
            return $"Success? {IsSuccess} (#messages: {Messages.Count})";
        }
    }
}
