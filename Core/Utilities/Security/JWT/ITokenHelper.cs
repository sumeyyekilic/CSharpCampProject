using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //kull adı ve parola girdi
        //veri tabanında ilgli kullanıcının VT da claim lerini bulucak. orda json web token üretecek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
