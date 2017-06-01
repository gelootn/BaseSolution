using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Bo.Internal
{
    public class Response<T>
    {

        private List<T> _values;


        public Response()
        {
            
        }

        public Response(T item)
        {
            
        }

        public Response(List<T> items)
        {
            
        }

        public List<T> Values => _values ?? (_values = new List<T>());

        public T Value
        {
            get
            {
                return Values.Any() ? Values.First() : default(T);
            }
        }


    }


    public class Message
    {
        public string Message { get; set; }
        public MessageType Type { get; set; }
    }
}
