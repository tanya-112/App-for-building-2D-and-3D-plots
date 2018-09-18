using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSystemsModeling
{
    class SimpleFunction2D
    {
        internal Function GetFunction(string rightPartOfTheFunction)
        {
            string functionString = "f(x)=" + rightPartOfTheFunction;
            Function function = new Function(functionString);

            return function;
        }

        internal void GetAndStoreTheArgsValues(out double[] arguments, double from, double to, double step)
        {
            int argLength = (int)Math.Ceiling((to - from) / step);
            arguments = new double[argLength];
            double[] result = new double[argLength];
            double k = from;
            for (int i = 0; i < argLength; i++)
            {
                arguments[i] = Math.Round(k,6);
                k += step;
            }
        }

        internal double [] CalculateResults(double[] arguments, Function function)
        {
            double[] result = new double[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                Expression expr = ModelingClass.BuildNewExpr(new double[] { arguments[i] }, function);// строим выражения для функции
                result[i] = Math.Round(expr.calculate(),6);
            }
            return result;
        }
    }
}
