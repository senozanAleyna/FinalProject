using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidler için başlangıç
    //başka da bir şey dönmüyor başarılı mı değil mi ve bilgisi var sadece
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
