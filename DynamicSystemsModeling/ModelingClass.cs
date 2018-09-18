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
    abstract class ModelingClass
    {
        internal List<char> properFunctionString;
        internal List<List<char>> variablesInRightPart;
        internal List<int> numbersWithoutMinusInBrackets;
        internal List<char> independentVariables;
        internal List<string> variablesStr;
        internal List<int> numbersAfterMinusInBrackets;
        internal int amountOfVariablesToBeDefined;
        internal bool functionContainsParamN;


        internal void GetInitialDataForDiffEquation(TextBox equation_textBox, List<char> properFunctionString,
    List<List<char>> variablesInRightPart, ref List<int> numbersWithoutMinusInBrackets, ref List<char> independentVariables,
    ref List<string> variablesStr, ref List<int> numbersAfterMinusInBrackets, ref int amountOfVariablesToBeDefined)
        {
            string initialFunctionString = equation_textBox.Text; //изначальная, не отформатированная строка
            List<char> wordMinus = new List<char> { 'm', 'i', 'n', 'u', 's' };
            bool insideOfBrackets = false;
            bool rememberNextNumber = false; // когда примет значение true, запомним идущее за знаком "-" число
            int countVariables = 0;
            //numbersAfterMinusInBrackets - список чисел которые вычитаются в записи вида х[n-число]
            //variablesInRightPart = new - вспомогательный список всех названий переменных в правой части уравнения (их будем использовать при постороении функции)
            int numberOfVariablesList = 0; // счетчик для передвижения по списку variables

            variablesInRightPart.Add(new List<char>());
            double forTryParse = 0;
            bool lastAddedCharWasANumberInBrackets = false;
            bool thereIsMinusInBrackets = false;

            foreach (char ch in initialFunctionString)
            {
                if (ch != '[' && ch != ']' && ch != '-')
                {
                    properFunctionString.Add(ch);
                    if (ch != '+' && ch != '*' && ch != '/' && ch != '^')
                    {
                        if (double.TryParse(ch.ToString(), out forTryParse) == false || insideOfBrackets)
                        {
                            if (initialFunctionString.IndexOf(ch) + 1 != initialFunctionString.Length
                                   && (initialFunctionString.ElementAt(initialFunctionString.IndexOf(ch) + 1) == '[' || insideOfBrackets))
                                variablesInRightPart[numberOfVariablesList].Add(ch);
                            if (insideOfBrackets == false)
                                if (initialFunctionString.IndexOf(ch) + 1 == initialFunctionString.Length
                                    || (initialFunctionString.ElementAt(initialFunctionString.IndexOf(ch) + 1) != '['))
                                {
                                    if (ch != '(' && ch != ')' && ch != '.' && ch != ',' && ch != 'n')
                                        independentVariables.Add(ch);
                                }
                        }
                        if (rememberNextNumber)
                        {
                            int chToInt;
                            if (Int32.TryParse(ch.ToString(), out chToInt))
                            {
                                int indexOfCurrentCh;
                                indexOfCurrentCh = properFunctionString.IndexOf(ch);
                                if (!lastAddedCharWasANumberInBrackets && thereIsMinusInBrackets)
                                {
                                    numbersAfterMinusInBrackets.Add(chToInt);
                                    numbersWithoutMinusInBrackets.Add(-1); // при использовании будет сигналом того, что при текущем индексе нужно брать число из numbersAfterMinusInBrackets
                                }

                                else if (!lastAddedCharWasANumberInBrackets && thereIsMinusInBrackets == false)
                                {
                                    numbersWithoutMinusInBrackets.Add(chToInt);
                                    numbersAfterMinusInBrackets.Add(-1); // при использовании будет сигналом того, что при текущем индексе нужно брать число из numbersWithoutMinusInBrackets

                                }

                                else
                                {
                                    if (thereIsMinusInBrackets == false)
                                    {
                                        string numberToConcateWithCurrent = numbersWithoutMinusInBrackets.Last().ToString();
                                        numbersWithoutMinusInBrackets.RemoveAt(numbersWithoutMinusInBrackets.Count - 1);
                                        numbersWithoutMinusInBrackets.Add(Int32.Parse(string.Concat(numberToConcateWithCurrent, ch)));
                                    }
                                    else
                                    {
                                        string numberToConcateWithCurrent = numbersAfterMinusInBrackets.Last().ToString();
                                        numbersAfterMinusInBrackets.RemoveAt(numbersAfterMinusInBrackets.Count - 1);
                                        numbersAfterMinusInBrackets.Add(Int32.Parse(string.Concat(numberToConcateWithCurrent, ch)));
                                    }
                                }
                                lastAddedCharWasANumberInBrackets = true;
                            }
                        }

                    }
                }
                if (ch == '[')
                {
                    insideOfBrackets = true;
                    rememberNextNumber = true;
                    thereIsMinusInBrackets = false;
                }
                if (ch == ']')
                {
                    insideOfBrackets = false;
                    rememberNextNumber = false;
                    numberOfVariablesList++;
                    variablesInRightPart.Add(new List<char>());
                    lastAddedCharWasANumberInBrackets = false;
                    thereIsMinusInBrackets = false;
                }
                if (ch == '-')
                {
                    if (insideOfBrackets == true)
                    {
                        properFunctionString.AddRange(wordMinus);
                        variablesInRightPart[numberOfVariablesList].AddRange(wordMinus);
                        lastAddedCharWasANumberInBrackets = false;
                        thereIsMinusInBrackets = true;
                        rememberNextNumber = true;
                        countVariables++;
                    }
                    else
                    {
                        properFunctionString.Add(ch);
                        lastAddedCharWasANumberInBrackets = false;
                    }
                }
            }

            variablesInRightPart.RemoveAt(numberOfVariablesList);// удаляем последний элемент, т.к. он пустой

            //преобразуем список списков символов в список строк для удобства использования данных
            for (int i = 0; i < variablesInRightPart.Count; i++)
            {
                variablesStr.Add(string.Concat(variablesInRightPart[i]));
            }

            variablesStr = AssureUniqueParams(variablesStr);
            if (independentVariables != null)
                independentVariables = AssureUniqueParams(independentVariables);
            numbersAfterMinusInBrackets = AssureUniqueParams(numbersAfterMinusInBrackets);
            numbersWithoutMinusInBrackets = AssureUniqueParams(numbersWithoutMinusInBrackets);

            /*
             даже если в формуле пропущены некоторые значения переменной в какие-то из моментов времени, 
             эти значения все равно будут необходимы при вычислении значений переменной в последующие моменты времени
            */
            List<int> numbersAfterMinusInBracketsExtended = new List<int>(); // список чисел в записи x[n-число], дополненный пропущенными в формуле
            numbersAfterMinusInBracketsExtended = FindAndPasteMissingNumbers(numbersAfterMinusInBrackets);

            amountOfVariablesToBeDefined = numbersAfterMinusInBracketsExtended.Count();
        }

       
        internal virtual void GetAndStoreTheArgsValues(ref double[] arrayToStoreIn, int amountOfVariablesToBeDefined, TextBox[] initialConditionsTextBoxes)
        {
            for (int i = 0; i < amountOfVariablesToBeDefined; i++)
            {
                double value;
                if (double.TryParse(initialConditionsTextBoxes[i].Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out value))
                    arrayToStoreIn[i] = value;//CultureInfo - чтобы в качестве разделителя в числе воспринималась точка
                else// если начальное условие выражается разностным уравнением (надо выделить параллельно с этим случай обычной функции!)
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
                        Function functionForInitCondition = new Function(functionStringForInitCondition);
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
                                functionForInitCondition = new Function(functionStringForInitCondition);
                                fInitConditionContainsParamN = true;
                            }
                        }
                        double[] argumentsForInitConditionsFunction = new double[variablesStrFor2DInitConditions.Count];
                        if (fInitConditionContainsParamN)
                            argumentsForInitConditionsFunction = new double[variablesStrFor2DInitConditions.Count + 1];
                        {
                            for (int k = 0; k < variablesStrFor2DInitConditions.Count; k++)
                            {
                                if (numbersAfterMinusInBrackets2DInitConditions[k] == -1)
                                    argumentsForInitConditionsFunction[k] = arrayToStoreIn[numbersWithoutMinusInBracketsInitConditions[k]];
                                else
                                    argumentsForInitConditionsFunction[k] = arrayToStoreIn[i - numbersAfterMinusInBrackets2DInitConditions[k]];
                            }
                            if (fInitConditionContainsParamN)
                                argumentsForInitConditionsFunction[argumentsForInitConditionsFunction.Length - 1] = i;

                            Expression expr = BuildNewExpr(argumentsForInitConditionsFunction, functionForInitCondition);
                            arrayToStoreIn[i] = Math.Round(expr.calculate(), 6);
                        }
                    }
                }
            }
        }

        internal static Expression BuildNewExpr(double[] arguments, Function f)
        {
            string[] properArguments = new string[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                string arg = arguments[i].ToString();
                string properArg = arg.Replace(',', '.');
                properArguments[i] = properArg;
            }
            string exprArg = "f(" + String.Format("{0}", String.Join(",", properArguments)) + ")";
            Expression expr = new Expression(exprArg, f);

            return expr;
        }
        private List<int> FindAndPasteMissingNumbers(List<int> sequenceOfNumbers)
        {
            List<int> sequenceOfNumbersCopy = new List<int>();
            for (int i = 0; i < sequenceOfNumbers.Count; i++)
            {
                if (sequenceOfNumbers[i] != -1)
                sequenceOfNumbersCopy.Add(sequenceOfNumbers[i]);
            }
            if (sequenceOfNumbersCopy.Count > 0)
            {
                //ищем максимальный элемент
                int max = 1;

                foreach (int number in sequenceOfNumbersCopy)
                {
                    if (number > max)
                        max = number;
                }
                if (max != sequenceOfNumbersCopy.Count)//значит, как минимум, один элемент пропущен
                {
                    if (sequenceOfNumbersCopy[0] != 1 && sequenceOfNumbersCopy[0] != 0)
                        sequenceOfNumbersCopy.Insert(0, 1);
                    int i = sequenceOfNumbersCopy.Count - 1;

                    while (i > 0)
                    {
                        int difference = sequenceOfNumbersCopy[i] - sequenceOfNumbersCopy[i - 1];
                        if (Math.Abs(difference) > 1)
                        {
                            if (difference > 1)
                            {
                                bool sequenceContainsTheNumber = false;
                                for (int k = i; k < sequenceOfNumbersCopy.Count; k++)
                                {
                                    if (sequenceOfNumbersCopy[k] == sequenceOfNumbersCopy[i - 1] + 1)
                                        sequenceContainsTheNumber = true;
                                }
                                if (!sequenceContainsTheNumber)
                                {
                                    sequenceOfNumbersCopy.Insert(i, sequenceOfNumbersCopy[i - 1] + 1);
                                    i+=2;
                                }
                            }
                            else
                            {
                                bool sequenceContainsTheNumber = false;
                                for (int k = i; k < sequenceOfNumbersCopy.Count; k++)
                                {
                                    if (sequenceOfNumbersCopy[k] == sequenceOfNumbersCopy[i - 1] - 1)
                                        sequenceContainsTheNumber = true;
                                }
                                if (!sequenceContainsTheNumber)
                                {
                                    sequenceOfNumbersCopy.Insert(i, sequenceOfNumbersCopy[i - 1] - 1);
                                    i+=2;
                                }
                            }
                        }
                        i--;
                    }
                }
            }
            return sequenceOfNumbersCopy;
        }

        private List<string> AssureUniqueParams(List<string> paramList)
        {
            return paramList.Distinct().ToList();
        }

        private List<char> AssureUniqueParams(List<char> paramList)
        {
            return paramList.Distinct().ToList();
        }
        private List<int> AssureUniqueParams(List<int> paramList)
        {
            return paramList.Distinct().ToList();
        }

    }
}
