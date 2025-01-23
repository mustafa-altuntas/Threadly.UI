using System.Text.Json.Serialization;

namespace Threadly.UI.DTOs
{


    public class ResponseDto<T> where T : class
    {
        [JsonPropertyName("Data")]
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessfull { get; set; }
        public List<string> Errors { get; set; }

        //public static ResponseDto<T> CreateInstance()
        //    => new ResponseDto<T>();

        //public static ResponseDto<T> Success(T data, int statusCode)
        //    => new ResponseDto<T> { StatusCode = statusCode, Data = data, IsSuccessfull = true };

        //public static ResponseDto<T> Success(int statusCode)
        //    => new ResponseDto<T> { StatusCode = statusCode, IsSuccessfull = true };

        //public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        //    => new ResponseDto<T> { Errors = errors, StatusCode = statusCode, IsSuccessfull = false };

        //public static ResponseDto<T> Fail(string error, int statusCode)
        //    => new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccessfull = false };



    }
}
