using System.Collections.Generic;

namespace BaselineSolution.Framework.Response
{
    public class ApiResponse<T> : BaseResponse<T>
    {

        public ApiResponse()
        {
            
        }

        public ApiResponse(T value) : base(value)
        {

        }

        public ApiResponse(ICollection<T> values): base(values)
        {

        }
    }
}
