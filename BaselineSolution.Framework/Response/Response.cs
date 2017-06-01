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
    public abstract class Response<T> where T : new()
    {
        private List<Message> _messages;
        private List<T> _values;

        protected Response(T value)
        {   
            Values.Add(value);
        }

        protected Response(ICollection<T> values)
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
                return new T();
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

        public virtual void AddError(string message, Exception exception = null)
        {
            var msg = new Message
            {
                Type = MessageType.Error,
                MessageText = message
            };

            if (exception != null) msg.Exception = exception;
            Messages.Add(msg);

        }
        public virtual void AddError(string message, Exception exception = null, params string[] args)
        {
            var msg = new Message
            {
                Type = MessageType.Error,
                MessageText = string.Format(message, args)
            };

            Messages.Add(msg);


        }
        public virtual void AddError(Message error)
        {
            Messages.Add(error);

        }
        public virtual void AddError(List<Message> errors)
        {
            Messages.AddRange(errors);
        }

        public virtual void AddNotFound(int id)
        {
            AddError($"item with id {id} was not found");
        }

        public virtual void AddError(List<string> errors)
        {
            Messages.AddRange(errors.Select(x => new Message {Type = MessageType.Error, MessageText = x}));
        }

        public virtual void AddSuccess(string message)
        {
            Messages.Add(new Message() { Type = MessageType.Success, MessageText = message });

        }

        public virtual void AddWarning(string message)
        {
            Messages.Add(new Message() { Type = MessageType.Warning, MessageText = message });

        }
        
        public override string ToString()
        {
            return $"Success? {IsSuccess} (#messages: {Messages.Count})";
        }
    }
}
