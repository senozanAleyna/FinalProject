using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //onbefore u aldık overrride ettik
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //bana bir validator type ver
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            { //gelen type  validator değilse ona kız
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType; //hata değilse validator type'ım budur
        }
        protected override void OnBefore(IInvocation invocation)
        {   //invocation =metod
            //çalışma anında instance üretmek(newlemek) için activator.createInstance kullanılır
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //reflection
            //metodda bir sürü imza olabilir mesela add'de product olur x olur bu yuzden ,
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//validatorun çalıştıgı tipi bul baseine bak çalıştıgı veri tipine bak
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//ilgili metodun parametrelerini bul, ama validatorun tipine eşit olanları
            foreach (var entity in entities)//bütün parametreleri gez
            {
                ValidationTool.Validate(validator, entity);// validation tool kullanarak doğrula
            }
        }
    }
}
