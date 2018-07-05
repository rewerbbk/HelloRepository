using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extensions
{
    public static class GenericExtensions
    {

        public static T WithRetry<T>(this Func<T> action, int attempts = 3)
        {
            var result = default(T);
            int retryCount = 0;

            bool succesful = false;
            do
            {
                try
                {
                    result = action();
                    succesful = true;
                }
                catch (Exception)
                {
                    retryCount++;
                }
            } while (retryCount < attempts && !succesful);

            return result;
        }

        //use this if you know the parameter value up front
        public static Func<TResult> Partial<TParam1, TResult>(
            this Func<TParam1, TResult> func, TParam1 parameter)
        {
            return () => func(parameter);
        }

        //use this to defer setting the paramater until exection.
        public static Func<TParam1, Func<TResult>> Curry<TParam1, TResult>
            (this Func<TParam1, TResult> func)
        {
            return parameter => () => func(parameter);
        }
    }
}
