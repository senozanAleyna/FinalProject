using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constans;

namespace Business.BusinessAspects.Autofac
{
    //JWT (Json Web Token)
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        //Context Accessor sayesinde aynı anda binlerce istek yapılabilir ve her kullanıcıya bir thread oluşturulur

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //Aspect yapıları katmanlı zincirin içinde olmadığı için Injection yapıldığında Web API tarafından görülmez
            //Service tool kullanır
        }



        //Methodun önünde çalışarak yetki kontrolü sağlar
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); 
            foreach (var role in _roles) 
            {
                if (roleClaims.Contains(role))//ilgili rol varsa metodu çalıştırmaya devam eder
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied); 
        }
    }
}
