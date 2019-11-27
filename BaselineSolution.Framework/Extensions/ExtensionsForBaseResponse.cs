using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.ApiHandling;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Framework.Extensions
{
        public static class ExtensionsForBaseResponse
    {
        public static void Error<T>(this BaseResponse<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.Error });
        }

        public static void Error<T>(this BaseResponse<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Error }));
        }

        public static void Error<T>(this BaseResponse<T> response, Exception ex)
        {
            response.Messages.Add(new Message { MessageText = ex.Message, Type = MessageType.Error });
            if(ex.InnerException != null)
                response.Error(ex.InnerException);
        }

        public static void Message<T>(this BaseResponse<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.None });
        }

        public static void Message<T>(this BaseResponse<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.None }));
        }

        public static void Success<T>(this BaseResponse<T> response, string message)
        {
            response.Messages.Add(new Message { MessageText = message, Type = MessageType.Success });
        }

        public static void Success<T>(this BaseResponse<T> response, List<string> messages)
        {
            response.Messages.AddRange(messages.Select(x => new Message { MessageText = x, Type = MessageType.Success }));
        }

        public static ApiResponse<TViewModel> ToApiResponse<TBo,TViewModel>(this BaseResponse<TBo> response) where TBo : BaseBo where TViewModel : ApiViewModel<TBo>, new()
        {
            var values = response.Values.Select(x => new TViewModel {Result = x});
            var api = new ApiResponse<TViewModel>(values) { Messages = response.Messages };
            
            return api;
        }

        public static ApiResponse<int> ToApiResponse(this BaseResponse<int> response)
        {
            var api = new ApiResponse<int>(response.Values) { Messages = response.Messages };
            return api;
        }

        public static ApiResponse<bool> ToApiResponse(this BaseResponse<bool> response)
        {
            var api = new ApiResponse<bool>(response.Values) { Messages = response.Messages };
            return api;
        }

        public static Response<T> ToResponse<T>(this BaseResponse<T> response)
        {
            var api = new Response<T>(response.Values) { Messages = response.Messages };
            return api;
        }
    }
}
