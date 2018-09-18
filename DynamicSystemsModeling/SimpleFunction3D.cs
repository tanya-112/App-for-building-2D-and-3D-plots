using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSystemsModeling
{
    class SimpleFunction3D
    {
        internal Function GetFunction(string rightPartOfTheFunction)
        {
            string functionString = "f(x,y)=" + rightPartOfTheFunction;
            Function function = new Function(functionString);

            return function;
        }


        internal void GetAndStoreTheArgsValues(out double[] argumentsX, out double[] argumentsY, double xFrom, double xTo,
            double yFrom, double yTo, double step)
        {
            int x_argLength_simleFunction3D = (int)(Math.Ceiling((xTo - xFrom + 1) / step) - step);
            int y_argLength_simleFunction3D = (int)(Math.Ceiling((yTo - yFrom + 1) / step) - step);
            argumentsX = new double[x_argLength_simleFunction3D];
            argumentsY = new double[y_argLength_simleFunction3D];
            double kX = Form1.x_from_simleFunction3D;
            double kY = Form1.y_from_simleFunction3D;
            for (int i = 0; i < x_argLength_simleFunction3D; i++)
            {
                argumentsX[i] = kX;
                kX += step;
            }
            for (int j = 0; j < y_argLength_simleFunction3D; j++)
            {
                argumentsY[j] = kY;
                kY += step;
            }
        }

        internal List<List<double>> CalculateResults(double[] argumentsX, double[] argumentsY, Function function)
        {
            List<List<double>> output3D = new List<List<double>>();
            output3D.Add(new List<double>());// x
            output3D.Add(new List<double>());// y
            output3D.Add(new List<double>());// z

            double[] localArgs = new double[2];
            Expression exp;
            for (int i = 0; i < argumentsX.Length; i++)
            {
                localArgs[0] = argumentsX[i];
                for (int j = 0; j < argumentsY.Length; j++)
                {
                    localArgs[1] = argumentsY[j];
                    exp = ModelingClass.BuildNewExpr(localArgs, function);// строим выражения для функции

                    output3D[0].Add(argumentsX[i]);
                    output3D[1].Add(argumentsY[j]);
                    output3D[2].Add(exp.calculate());
                }
            }
            return output3D;
        }
    }
}
