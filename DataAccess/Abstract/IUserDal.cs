using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);  //operasyon ? çünk burda join atılacak
        //neden join atılacak : amaç kullanıcının sisteme eklenmesi vs bunlar zaten olacak, 
        //aynı zamanda vt dan  operation claim lerini çekmek istiyorum.

    }
}
