using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        
        //cons içinde set oluşturmuş olduk. ! standartize etmiş olduk
        //böylece adamlar kafasına göre set yapamayacak.
        public Result(bool success, string message):this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; } 

        public string Message { get; }
    }
}
