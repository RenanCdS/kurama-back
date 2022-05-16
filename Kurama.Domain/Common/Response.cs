using System.Collections.Generic;

namespace Kurama.Domain.Common
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T Value { get; set; }

        public Response()
        {

        }

        public Response(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static Response<T> Ok(T value)
        {
            var response = new Response<T>(true);
            response.Value = value;
            return response;
        }

        public static Response<T> Ok()
        {
            var response = new Response<T>(true);
            return response;
        }

        public static Response<T> Fail()
        {
            var response = new Response<T>(false);
            return response;
        }

        public static Response<T> Fail(string message)
        {
            var response = new Response<T>(false);
            response.Messages.Add(message);
            return response;
        }
    }
}
