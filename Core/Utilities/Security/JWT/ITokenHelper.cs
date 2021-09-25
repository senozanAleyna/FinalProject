using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //buradaki amacım bir token uretmektir.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);

        // sisteme kullanıcı adı ve sıfresı gırıldı. API'de bu operasyon calıscak
        //doğruysa ılgılı kullanıcı ıcın verıtabanına gıdecek claimlerini bulacak orda bır tane JWT üretecek
        //onları sisteme verecek !

        //bunu interface ile yaptık çünkü yarın öbür gün başka bir şeyle çalışabilirim

        //coreda entitye bağımlı olmayalım diye userı vs core a geçirdik
    }
}
