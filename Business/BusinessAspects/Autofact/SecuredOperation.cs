using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Business.Constants;

namespace Business.BusinessAspects.Autofact
{
    //JWT
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles) //rolleri ver diyo. attribute lerde virgul ile ayrılarak geliyor.
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //o anki kullanıcının claim rollerini çöz
            foreach (var role in _roles//kullanıcını rollerini gez. 
            {
                if (roleClaims.Contains(role)) //claimlerin içinde ilgili rol varsa retrn et
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //yoksa  AuthorizationDenied :yetkin yok hatası ver.
        }
    }
}
