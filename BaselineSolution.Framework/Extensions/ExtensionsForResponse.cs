using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForResponse
    {
        public static Response<T> AddErrorMessage<T>(this Response<T> response, string message)
        {
            response.Messages.Add(new Message{ MessageText = message, Type = MessageType.Error});
            return response;
        }

        public static Response<T> AddErrorMessage<T>(this Response<T> response, Exception ex)
        {
            response.Messages.Add(new Message{ MessageText = ex.Message, Type = MessageType.Error});
            if (ex.InnerException != null)
                response.AddErrorMessage(ex.InnerException);

            return response;
        }

        public static Response<T> AddErrorMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Error }));
            return response;
        }

        public static Response<T> AddItemNotFound<T>(this Response<T> response, int id)
        {
            return response.AddErrorMessage($"Item with Id {id} was not found");
        }

        public static Response<T> AddMessage<T>(this Response<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.None });
            return response;
        }

        public static Response<T> AddMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.None }));
            return response;
        }

        public static Response<T> AddSuccessMessage<T>(this Response<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.Success });
            return response;
        }

        public static Response<T> AddSuccessMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Success }));
            return response;
        }
    }



}
