using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Bo.Internal.Extensions
{
    public static class ExtensionsForResponse
    {
        public static Response<T> AddValidationMessage<T>(this Response<T> response, ValidationMessage message)
        {
            response.Messages.Add(new Message { MessageText = message.Message, FieldName = message.FieldName, Type = MessageType.Validation });
            return response;
        }

        public static Response<T> AddValidationMessage<T>(this Response<T> response, List<ValidationMessage> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x.Message, FieldName = x.FieldName, Type = MessageType.Validation }));
            return response;
        }
    }
}
