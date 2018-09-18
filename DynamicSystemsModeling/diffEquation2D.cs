using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSystemsModeling
{
    class diffEquation2D : ModelingClass
    {

        internal Function GetFunction()
        {
            string functionString = String.Format("f({0})=", String.Join(", ", variablesStr)) + string.Concat(properFunctionString);
            Function function = new Function(functionString);

            functionContainsParamN = false;

            if (properFunctionString.Contains('n'))
            {
                int indexOfN;
                indexOfN = properFunctionString.IndexOf('n');
                if ((indexOfN > 0 && (properFunctionString.ElementAt(indexOfN - 1) == '*' ||
                    properFunctionString.ElementAt(indexOfN - 1) == '+' || properFunctionString.ElementAt(indexOfN - 1) == '-'
                    || properFunctionString.ElementAt(indexOfN - 1) == '^' || properFunctionString.ElementAt(indexOfN - 1) == '/'))
                    || indexOfN < properFunctionString.Count - 1 && (properFunctionString.ElementAt(indexOfN + 1) == '*' ||
                    properFunctionString.ElementAt(indexOfN + 1) == '+' || properFunctionString.ElementAt(indexOfN + 1) == '-'
                    || properFunctionString.ElementAt(indexOfN + 1) == '^' || properFunctionString.ElementAt(indexOfN + 1) == '/'))
                {
                    functionString = String.Format("f({0},{1})=", String.Join(", ", variablesStr), 'n') + string.Concat(properFunctionString);
                    function = new Function(functionString);
                    functionContainsParamN = true;
                }
            }
            return function;
        }


        //result передается с ключ. словом ref, т.к. он зачастую уже частично заполнен (аргументами) и его нужно дозаполнять рез-ми вычислений
        internal void CalculateResults(ref double[] result, Function function, int countTill)
        {
            double [] arguments = new double[variablesStr.Count];
            if (functionContainsParamN)
                arguments = new double[variablesStr.Count + 1];
            for (int i = amountOfVariablesToBeDefined; i <= countTill; i++) 
            {
                for (int k = 0; k < variablesStr.Count; k++)
                {
                    if (numbersAfterMinusInBrackets[k] == -1)
                        arguments[k] = result[numbersWithoutMinusInBrackets[k]]; 
                    else
                        arguments[k] = result[i - numbersAfterMinusInBrackets[k]];
                }
                if (functionContainsParamN)
                    arguments[arguments.Length - 1] = i;
                Expression expr = BuildNewExpr(arguments, function);
                result[i] = Math.Round(expr.calculate(), 6);
            }
        }


    }
}
