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
    public class Response<T> : BaseResponse<T>
    {


        public Response()
        {

        }

        public Response(T value) : base(value)
        {

        }

        public Response(ICollection<T> values) : base(values)
        {

        }



        public bool HasValue => Values.Count != 0;

        public T Value
        {
            get
            {
                if (Values != null && Values.Count == 1)
                    return Values.First();
                return default(T);
            }

        }

        public bool IsSuccess
        {
            get
            {
                return Messages.Any(c => c.Type == MessageType.Error || c.Type == MessageType.Validation) == false;
            }
        }
        public bool HasWarnings
        {
            get { return Messages.Any(c => c.Type == MessageType.Warning); }
        }



        public override string ToString()
        {
            return $"Success? {IsSuccess} (#messages: {Messages.Count})";
        }
    }
}
