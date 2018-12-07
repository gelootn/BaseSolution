using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForResponse
    {
        public static Response<T> AddErrorMessage<T>(this Response<T> response, string message)
        {
            response.Error(message);
            return response;
        }

        public static Response<T> AddErrorMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Error(messages);
            return response;
        }

        public static Response<T> AddErrorMessage<T>(this Response<T> response, Exception ex)
        {
            response.Error(ex);
            return response;
        }

        public static Response<T> AddItemNotFound<T>(this Response<T> response, int id)
        {
            return response.AddErrorMessage($"Item with Id {id} was not found");
        }

        public static Response<T> AddMessage<T>(this Response<T> response, string message)
        {
            response.Message(message);
            return response;
        }

        public static Response<T> AddMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Message(messages);
            return response;
        }

        public static Response<T> AddSuccessMessage<T>(this Response<T> response, string message)
        {
            response.Success(message);
            return response;
        }

        public static Response<T> AddSuccessMessage<T>(this Response<T> response, List<string> messages)
        {
            response.Success(messages);
            return response;
        }

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
