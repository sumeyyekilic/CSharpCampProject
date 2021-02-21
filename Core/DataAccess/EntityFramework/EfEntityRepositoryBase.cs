using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    class EfEntityRepositoryBase<TEntity, TContext>
        where TEntity: class , IEntity, new()
        where TContext: DbContext , new() //Dbcontext entity fw'den gelir.
    {

    }
}
