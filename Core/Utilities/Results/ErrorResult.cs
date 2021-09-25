using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)//success olduğu için direkt true yolladık
        {

        }
        public ErrorResult() : base(false)//mesaj vermedik
        {

        }
    }
}
