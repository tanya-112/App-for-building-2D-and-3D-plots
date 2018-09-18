using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using nzy3D.Maths;
using DynamicSystemsModeling;
using org.mariuszgromada.math.mxparser;


namespace nzy3D.Plot3D.Builder.Concrete
{

        public class myOrthonormalGrid : OrthonormalGrid
    {

            public myOrthonormalGrid(Range xrange, int xsteps, Range yrange, int ysteps) : base(xrange, xsteps, yrange, ysteps)
        {
            }

            public myOrthonormalGrid(Range xyrange, int xysteps) : base(xyrange, xysteps)
        {
            }

            public override System.Collections.Generic.List<Coord3d> Apply(Mapper mapper)
            {

                List<Coord3d> output = new List<Coord3d>();
          
            double[] result = new double[Form1.display3DTo + 1];

            if (Form1.simpleFunction3DMakingPlot == false)
            {
                if (Form1.parameterDependence_radioButton1.Checked) // если нужно построить график зависимости результатов в каждый момент времени от значения параметра

                {
                    Function function = Form1.dEquation3DVaryParam.GetFunction();
                    List<List<double>> output3D = Form1.dEquation3DVaryParam.CalculateResults(ref result, function,Form1.vary3DFrom,Form1.vary3DTo,Form1.vary3DStep,Form1.display3DFrom,Form1.display3DTo, Form1.initialConditionsTextBoxes3DVaryParameter);
                    for (int i = 0; i < output3D[0].Count; i++)
                         output.Add(new Coord3d(output3D[0][i], output3D[1][i], output3D[2][i]));
                }

                else if(Form1.initialValueDependence_radioButton1.Checked)
                {
                    Function function = Form1.dEquation3DInitialValue.GetFunction();
                    List<List<double>> output3D = Form1.dEquation3DInitialValue.CalculateResults(Form1.dEquation3DInitialValue, ref result, function, Form1.vary3DFrom, Form1.vary3DTo, Form1.vary3DStep, Form1.display3DFrom, Form1.display3DTo, Form1.initialConditionsTextBoxes3DVaryInitValue);
                    for (int i = 0; i < output3D[0].Count; i++)
                        output.Add(new Coord3d(output3D[0][i], output3D[1][i], output3D[2][i]));
                }
            }
                
            else //если нужно построить обычную фукнцию в 3D, а не по разностному уравнению
            {
                SimpleFunction3D simpleFunction3D = new SimpleFunction3D();
                Function function = simpleFunction3D.GetFunction(Form1.function_textBox3DText);                

                 double[] argumentsX;
                double[] argumentsY;
                simpleFunction3D.GetAndStoreTheArgsValues(out argumentsX, out argumentsY, Form1.x_from_simleFunction3D, Form1.x_to_simleFunction3D,
                    Form1.y_from_simleFunction3D, Form1.y_to_simleFunction3D, Form1.step_simleFunction3D);
                
                List<List<double>> output3D = simpleFunction3D.CalculateResults(argumentsX, argumentsY, function);
                for (int i = 0; i < output3D[0].Count; i++)
                    output.Add(new Coord3d(output3D[0][i], output3D[1][i], output3D[2][i]));           
            }  
                    
            return output;
            
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
