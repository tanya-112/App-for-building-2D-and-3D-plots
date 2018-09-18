using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicSystemsModeling
{
    class DiffEquationVaryParameter : ModelingClass
    {
        internal Function GetFunction()
        {
            string functionString = String.Format("f({0},{1})=", String.Join(", ", variablesStr), independentVariables[0]) + string.Concat(properFunctionString);
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
                    functionString = String.Format("f({0},{1},{2})=", String.Join(", ", variablesStr), independentVariables[0], 'n') + string.Concat(properFunctionString);
                    function = new Function(functionString);
                    functionContainsParamN = true;
                }
            }
            return function;
        }
        internal override void GetAndStoreTheArgsValues(ref double[] arrayToStoreIn, int amountOfVariablesToBeDefined, TextBox[] initialConditionsTextBoxes)
        {

        }
        private void GetAndStoreTheArgsValues(double currentParamValue, ref double[] arrayToStoreIn, int amountOfVariablesToBeDefined, TextBox[] initialConditionsTextBoxes)
        {
            for (int i = 0; i < amountOfVariablesToBeDefined; i++)
            {
                double value;
                if (double.TryParse(initialConditionsTextBoxes[i].Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out value))
                    arrayToStoreIn[i] = value;//CultureInfo - чтобы в качестве разделителя в числе воспринималась точка
                else// если начальное условие выражается разностным уравнением (можно выделить параллельно с этим случай обычной функции!)
                {
                    List<char> properFunctionStringForInitConditions = new List<char>();
                    List<List<char>> variablesInTheRightPartInitConditions = new List<List<char>>();
                    List<int> numbersWithoutMinusInBracketsInitConditions = new List<int>();
                    List<string> variablesStrFor2DInitConditions = new List<string>();
                    List<int> numbersAfterMinusInBrackets2DInitConditions = new List<int>();
                    int amountOfVariablesToBeDefined2DInitConditions = 0;

                    List<char> independentVariablesInitConditions = new List<char>();

                    if (initialConditionsTextBoxes[i].Text.Contains('[') || independentVariables.Count > 0)
                    {

                        GetInitialDataForDiffEquation(initialConditionsTextBoxes[i], properFunctionStringForInitConditions, variablesInTheRightPartInitConditions,
                    ref numbersWithoutMinusInBracketsInitConditions, ref independentVariablesInitConditions, ref variablesStrFor2DInitConditions,
                    ref numbersAfterMinusInBrackets2DInitConditions, ref amountOfVariablesToBeDefined2DInitConditions);


                        bool fInitConditionContainsParamN = false;

                        string functionStringForInitCondition = String.Format("f({0})=", String.Join(", ", variablesStrFor2DInitConditions)) + string.Concat(properFunctionStringForInitConditions);

                        if (independentVariablesInitConditions.Count > 0)
                            functionStringForInitCondition = String.Format("f({0},{1})=", String.Join(", ", variablesStrFor2DInitConditions), independentVariables[0]) + string.Concat(properFunctionStringForInitConditions);

                        if (properFunctionStringForInitConditions.Contains('n'))
                        {
                            int indexOfN;
                            indexOfN = properFunctionStringForInitConditions.IndexOf('n');
                            if ((indexOfN > 0 && (properFunctionStringForInitConditions.ElementAt(indexOfN - 1) == '*' ||
                                properFunctionStringForInitConditions.ElementAt(indexOfN - 1) == '+' || properFunctionStringForInitConditions.ElementAt(indexOfN - 1) == '-'
                                || properFunctionStringForInitConditions.ElementAt(indexOfN - 1) == '^' || properFunctionStringForInitConditions.ElementAt(indexOfN - 1) == '/'))
                                || indexOfN < properFunctionStringForInitConditions.Count - 1 && (properFunctionStringForInitConditions.ElementAt(indexOfN + 1) == '*' ||
                                properFunctionStringForInitConditions.ElementAt(indexOfN + 1) == '+' || properFunctionStringForInitConditions.ElementAt(indexOfN + 1) == '-'
                                || properFunctionStringForInitConditions.ElementAt(indexOfN + 1) == '^' || properFunctionStringForInitConditions.ElementAt(indexOfN + 1) == '/'))
                            {
                                functionStringForInitCondition = String.Format("f({0},{1})=", String.Join(", ", variablesStrFor2DInitConditions), 'n') + string.Concat(properFunctionStringForInitConditions);
                                if(independentVariablesInitConditions.Count > 0)
                                    functionStringForInitCondition = String.Format("f({0},{1},{2})=", String.Join(", ", variablesStrFor2DInitConditions), independentVariables[0], 'n') + string.Concat(properFunctionStringForInitConditions);
                                fInitConditionContainsParamN = true;
                            }
                        }

                        Function functionForInitCondition = new Function(functionStringForInitCondition);

                        double[] argumentsForInitConditionsFunction = new double[variablesStrFor2DInitConditions.Count + string.Concat(independentVariablesInitConditions).Length];
                        if (fInitConditionContainsParamN)
                            argumentsForInitConditionsFunction = new double[variablesStrFor2DInitConditions.Count + string.Concat(independentVariablesInitConditions).Length + 1];
                        {
                            for (int k = 0; k < variablesStrFor2DInitConditions.Count; k++)
                            {
                                if (numbersAfterMinusInBrackets2DInitConditions[k] == -1)
                                    argumentsForInitConditionsFunction[k] = arrayToStoreIn[numbersWithoutMinusInBracketsInitConditions[k]];
                                else
                                    argumentsForInitConditionsFunction[k] = arrayToStoreIn[i - numbersAfterMinusInBrackets2DInitConditions[k]];
                            }
                            if (independentVariablesInitConditions.Count > 0 && fInitConditionContainsParamN == false)
                                argumentsForInitConditionsFunction[argumentsForInitConditionsFunction.Length - 1] = currentParamValue;

                            if(fInitConditionContainsParamN && independentVariablesInitConditions.Count == 0)
                                argumentsForInitConditionsFunction[argumentsForInitConditionsFunction.Length - 1] = i;

                            if (fInitConditionContainsParamN && independentVariablesInitConditions.Count > 0)
                            {
                                argumentsForInitConditionsFunction[argumentsForInitConditionsFunction.Length - 2] = currentParamValue;
                                argumentsForInitConditionsFunction[argumentsForInitConditionsFunction.Length - 1] = i;
                            }

                            Expression expr = BuildNewExpr(argumentsForInitConditionsFunction, functionForInitCondition);
                            arrayToStoreIn[i] = Math.Round(expr.calculate(), 6);
                        }
                    }
                }
            }
        }

        internal List<List<double>> CalculateResults(ref double[] result, Function function,double varyParamFrom, double varyParamTo, double paramStep,
            int displayResultsFrom, int displayResultsTill, TextBox[] initialConditionsTextBoxes)
        {
            List<List<double>> output3D = new List<List<double>>();
            output3D.Add(new List<double>());// x
            output3D.Add(new List<double>());// y
            output3D.Add(new List<double>());// z

            double[] arguments = new double[variablesStr.Count + string.Concat(independentVariables).Length];
            if (functionContainsParamN)
                arguments = new double[variablesStr.Count + string.Concat(independentVariables).Length + 1];

            for (double q = varyParamFrom; q <= varyParamTo; q += paramStep)
            {
                //заново вычисляем значение всех аргументов, заданных пользователем, т.к. один из аргументов может зависеть от текущего параметра 
                GetAndStoreTheArgsValues(q, ref result, amountOfVariablesToBeDefined, initialConditionsTextBoxes);

                for (int i = amountOfVariablesToBeDefined; i <= displayResultsTill; i++) 
                {
                    for (int k = 0; k < variablesStr.Count; k++)
                    {
                        if (numbersAfterMinusInBrackets[k] == -1)
                            arguments[k] = result[numbersWithoutMinusInBrackets[k]]; 
                        else
                            arguments[k] = result[i - numbersAfterMinusInBrackets[k]];
                    }
                    if (functionContainsParamN)
                    {
                        arguments[arguments.Length - 2] = q;
                        arguments[arguments.Length - 1] = i;
                    }
                    else
                        arguments[arguments.Length - 1] = q;

                    Expression expr = BuildNewExpr(arguments, function);
                    result[i] = Math.Round(expr.calculate(), 6);
                    if (i >= displayResultsFrom)
                    {
                        output3D[0].Add(i);
                        output3D[1].Add(q);
                        output3D[2].Add(result[i]);
                    }
                }
            }
            return output3D;
        }
    }
}
