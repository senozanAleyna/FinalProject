using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //params verdiğiniz zaman Run içine istediğiniz kadar IResult verebiliyorsunuz
        //yani ilgili metodda birkaç tane kural yollayabilirim
        public static IResult Run(params IResult[] logics)
        {
            //parametreyle gelen iş kurallarını döndürüyoruz, hatalı olanı business'a bildiriyoruz.
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
