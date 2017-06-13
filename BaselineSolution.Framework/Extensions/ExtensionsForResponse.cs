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

        public static Response<T> AddErrorMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Error }));
            return response;
        }

        public static Response<T> AddValidationMessage<T>(this Response<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.Validation });
            return response;
        }

        public static Response<T> AddValidationMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Validation }));
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
    }



}
