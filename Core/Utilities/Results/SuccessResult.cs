using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)//success olduğu için direkt true yolladık
        {

        }
        public SuccessResult() : base(true)//mesaj vermedik
        {

        }
    }
}
