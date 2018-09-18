using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;
using System.Globalization;
using ZedGraph;
using nzy3D.Chart;
using nzy3D.Chart.Controllers.Thread.Camera;
using nzy3D.Colors;
using nzy3D.Colors.ColorMaps;
using nzy3D.Maths;
using nzy3D.Plot3D.Builder;
using nzy3D.Plot3D.Builder.Concrete;
using nzy3D.Plot3D.Primitives;
using nzy3D.Plot3D.Primitives.Axes.Layout;
using nzy3D.Plot3D.Rendering.Canvas;
using nzy3D.Plot3D.Rendering.View;



namespace DynamicSystemsModeling
{
    public partial class Form1 : Form
    {
        diffEquation2D dEquation2D;
        internal static DiffEquationVaryParameter dEquation3DVaryParam;
        internal static DiffEquationVaryInitialValue dEquation3DInitialValue;

        internal static RadioButton parameterDependence_radioButton1;
        internal static RadioButton initialValueDependence_radioButton1;


        System.Drawing.Color color = System.Drawing.Color.Black;
        GraphPane pane;

        System.Windows.Forms.Label[] initialConditionsLabels;

        internal static TextBox[] initialConditionsTextBoxes2D;

        internal static TextBox[] initialConditionsTextBoxes3DVaryParameter;

        internal static TextBox[] initialConditionsTextBoxes3DVaryInitValue;


        string[] dublicateInitialConditionsTextBoxesText2D;
        string[] dublicateInitialConditionsTextBoxesText3DVaryParameter;
        string[] dublicateInitialConditionsTextBoxesText3DVaryInitValue;

        internal static int display3DFrom;
        internal static int display3DTo;
        internal static double vary3DFrom;
        internal static double vary3DTo;
        internal static double vary3DStep;
        internal static List<char> independentVariables2D;
        internal static List<char> independentVariables3D;
        internal static List<char> independentVariablesInitConditions;
       
        internal static bool simpleFunction3DMakingPlot;
        internal static string function_textBox3DText;
        internal static double x_from_simleFunction3D;
        internal static double x_to_simleFunction3D;
        internal static double y_from_simleFunction3D;
        internal static double y_to_simleFunction3D;
        internal static double step_simleFunction3D;
      

        System.Drawing.Point x0_textBox_DefLocation;
        System.Drawing.Point x0_label_DefLocation;
        System.Drawing.Point from_diffEquation_textBox_DefLocation;
        System.Drawing.Point to_diffEquation_textBox_DefLocation;
        System.Drawing.Point from_diffEquation_label_DefLocation;
        System.Drawing.Point to_diffEquation_label_DefLocation;
        System.Drawing.Point color_diffEquation_label_DefLocation;
        System.Drawing.Point color_diffEquation_button_DefLocation;
        System.Drawing.Point markPoints_checkBox_DefLocation;
        System.Drawing.Point makePlot_diffEquation_button_DefLocation;
        System.Drawing.Point eraseData_diffEquation_button_DefLocation;


        System.Drawing.Point x0_textBox_DefLocation3D;
        System.Drawing.Point x0_label_DefLocation3D;
        System.Drawing.Point varyParameter_label_DefLocation3D;
        System.Drawing.Point displayResults_label_DefLocation3D;
        System.Drawing.Point vary3DFrom_textBoxDefLocation3D;
        System.Drawing.Point vary3DTo_textBoxDefLocation3D;
        System.Drawing.Point vary3DStep_textBoxDefLocation3D;
        System.Drawing.Point vary3DFrom_labelDefLocation3D;
        System.Drawing.Point vary3DTo_labelDefLocation3D;
        System.Drawing.Point vary3DStep_labelDefLocation3D;
        System.Drawing.Point display3DFrom_textBoxDefLocation3D;
        System.Drawing.Point display3DTo_textBoxDefLocation3D;
        System.Drawing.Point display3DFrom_labelDefLocation3D;
        System.Drawing.Point display3DTo_labelDefLocation3D;       

        private CameraThreadController t;
        private IAxeLayout axeLayout;

        public Form1()
        {
            InitializeComponent();

            dEquation2D = new diffEquation2D()
            {
                properFunctionString = new List<char>(),
                variablesInRightPart = new List<List<char>>(),
                numbersWithoutMinusInBrackets = new List<int>(),
                independentVariables = new List<char>(),
                variablesStr = new List<string>(),
                numbersAfterMinusInBrackets = new List<int>(),
                amountOfVariablesToBeDefined = new Int32(),
                functionContainsParamN = new bool()
            };

            dEquation3DVaryParam = new DiffEquationVaryParameter()
            {
                properFunctionString = new List<char>(),
                variablesInRightPart = new List<List<char>>(),
                numbersWithoutMinusInBrackets = new List<int>(),
                independentVariables = new List<char>(),
                variablesStr = new List<string>(),
                numbersAfterMinusInBrackets = new List<int>(),
                amountOfVariablesToBeDefined = new Int32(),
                functionContainsParamN = new bool()
            };
            dEquation3DInitialValue = new DiffEquationVaryInitialValue()
            {
                properFunctionString = new List<char>(),
                variablesInRightPart = new List<List<char>>(),
                numbersWithoutMinusInBrackets = new List<int>(),
                independentVariables = new List<char>(),
                variablesStr = new List<string>(),
                numbersAfterMinusInBrackets = new List<int>(),
                amountOfVariablesToBeDefined = new Int32(),
                functionContainsParamN = new bool()
            };

            parameterDependence_radioButton1 = parameterDependence_radioButton;
            initialValueDependence_radioButton1 = initialValueDependence_radioButton;



            x0_textBox_DefLocation = x0_textBox.Location;
            x0_label_DefLocation = x0_label.Location;
            from_diffEquation_textBox_DefLocation = from_diffEquation_textBox.Location;
            to_diffEquation_textBox_DefLocation = to_diffEquation_textBox.Location;
            from_diffEquation_label_DefLocation = from_diffEquation_label.Location;
            to_diffEquation_label_DefLocation = to_diffEquation_label.Location;
            color_diffEquation_label_DefLocation = color_diffEquation_label.Location;
            color_diffEquation_button_DefLocation = color_diffEquation_button.Location;
            markPoints_checkBox_DefLocation = markPoints_checkBox.Location;
            makePlot_diffEquation_button_DefLocation = makePlot_diffEquation_button.Location;
            eraseData_diffEquation_button_DefLocation = eraseData_diffEquation_button.Location;


            x0_textBox_DefLocation3D = x0_textBox3D.Location;
            x0_label_DefLocation3D = x0_label3D.Location;
            varyParameter_label_DefLocation3D = varyParameter_label.Location;
            vary3DFrom_textBoxDefLocation3D = vary3DFrom_textBox.Location;
            vary3DTo_textBoxDefLocation3D = vary3DTo_textBox.Location;
            vary3DStep_textBoxDefLocation3D = vary3DStep_textBox.Location;
            vary3DFrom_labelDefLocation3D = vary3DFrom_label.Location;
            vary3DTo_labelDefLocation3D = vary3DTo_label.Location;
            vary3DStep_labelDefLocation3D = vary3DStep_label.Location;
            displayResults_label_DefLocation3D = displayResults_label.Location;
            display3DFrom_textBoxDefLocation3D = display3DFrom_textBox.Location;
            display3DFrom_labelDefLocation3D = display3DFrom_label.Location;
            display3DTo_labelDefLocation3D = display3DTo_label.Location;
            display3DTo_textBoxDefLocation3D = display3DTo_textBox.Location;

            pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "\n";
            pane.XAxis.Title.Text = "X";
            pane.YAxis.Title.Text = "Y";

        }

        private void color_button_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                color = colorDialog1.Color;
        }

        private void makePlot_button_Click(object sender, EventArgs e)
        {
            dublicateInitialConditionsTextBoxesText2D = new string[dEquation2D.amountOfVariablesToBeDefined];
            if (initialConditionsTextBoxes2D != null)
                for (int i = 0; i < initialConditionsTextBoxes2D.Length; i++) 
            {
                dublicateInitialConditionsTextBoxesText2D[i] = initialConditionsTextBoxes2D[i].Text;
            }


            int from = Int32.Parse(from_diffEquation_textBox.Text);
            int to = Int32.Parse(to_diffEquation_textBox.Text);

            // посчитаем и сохраним все значения для разностного уравнения от 0 до значения to в массиве result
            // но выведем пользователю на график только интересующие его значения от from до to
            double[] result = new double[to + 1];
            double[] resultToDisplay = new double[to - from + 1];


            Function function = dEquation2D.GetFunction();

            // запоминаем все заданные пользователем начальные условия
            dEquation2D.GetAndStoreTheArgsValues(ref result, dEquation2D.amountOfVariablesToBeDefined, initialConditionsTextBoxes2D);

            dEquation2D.CalculateResults(ref result, function, to);

            double[] argsToDisplay = new double[resultToDisplay.Length];
            int j = 0;
            for (int i = from; i <= to; i++)
            {
                resultToDisplay[j] = result[i];
                argsToDisplay[j] = i;
                j++;
            }
            if (markPoints_checkBox.Checked)
                Draw(argsToDisplay, resultToDisplay, 1, "x[n] = " + equation_textBox2D.Text);
            else
                Draw(argsToDisplay, resultToDisplay, 1, "x[n] = " + equation_textBox2D.Text, SymbolType.None);

        }
      

        private void Draw(double[] arguments, double[] result, double step, string functionName, SymbolType symbolType = SymbolType.Circle, string xAxisTitle = "n", string yAxisTitle = "X")
        {
            // Создадим список точек
            PointPairList list = new PointPairList();
            
            // Заполняем список точек
            for (int i = 0; i < result.Length; i++)
            {
                list.Add(arguments[i], result[i]);
            }

            // Создадим кривую 
            LineItem myCurve = pane.AddCurve(functionName, list, color, symbolType);
            pane.XAxis.Title.Text =xAxisTitle;
            pane.YAxis.Title.Text = yAxisTitle;
            pane.Legend.FontSpec.Size = 15;
            pane.Title.Text = "";

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            // В противном случае на рисунке будет показана только часть графика, 
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
            erasePlot_button.Enabled = true;
        }


        //контролируем кликабельность кнопок
        #region
        private void equation_textBox_TextChanged(object sender, EventArgs e)  
        {
            string argString = "";
            if (sender == equation_textBox2D)
                argString = "diffEquation2D";
            else if (sender == function_textBox)
                argString = "simpleFunction2D";
            else if (sender == equation_textBox3D)
                argString = "diffEquation3D";
            else if (sender == function_textBox3D)
                argString = "simpleFunction3D";

            ControlButtons(argString);
        }
        private void x0_textBox_TextChanged(object sender, EventArgs e)
        {
            if (sender == x0_textBox)
                ControlButtons("diffEquation2D");
            else if (sender == x0_textBox3D)
                ControlButtons("diffEquation3D");
            else
            {
                bool senderIsFrom2DPage = false;
                foreach (Control control in differenceEquation_tabPage.Controls)
                {
                    if (control == sender)
                        senderIsFrom2DPage = true;
                }
                if (senderIsFrom2DPage)
                    ControlButtons("diffEquation2D");
                else
                    foreach (Control control in differenceEquation3D_tabPage.Controls)
                {
                        if (control == sender)
                        {
                            ControlButtons("diffEquation3D");
                            break;
                        }
                }
            }

        }

        private void from_textBox_TextChanged(object sender, EventArgs e)
        {
            string argString = "";
            if (sender == from_diffEquation_textBox)
                argString = "diffEquation2D";
            else if (sender == from_simpleFunction_textBox)
                argString = "simpleFunction2D";

            ControlButtons(argString);
        }

        private void to_textBox_TextChanged(object sender, EventArgs e)
        {
            string argString = "";
            if (sender == to_diffEquation_textBox)
                argString = "diffEquation2D";
            else if (sender == to_simpleFunction_textBox)
                argString = "simpleFunction2D";

            ControlButtons(argString);
        }

        private void ControlButtons(string senderPageName)
        {
            if (senderPageName == "diffEquation2D")
            {
                double isNum;
                bool isNum1 = Double.TryParse(x0_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum2 = Double.TryParse(from_diffEquation_textBox.Text, out isNum);
                bool isNum3 = Double.TryParse(to_diffEquation_textBox.Text, out isNum);
                bool initialTextBoxFilled = true;
                bool conditionsDataIsCorrect = true;

                if (initialConditionsTextBoxes2D != null)
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes2D)
                    {
                        bool currTextBoxDataIsCorrect = true;
                        if (textbox.Text == "")
                        {
                            initialTextBoxFilled = false;
                        }
                        else
                            currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }
                bool equationIsCorrect = CheckFormulaCorrectness(equation_textBox2D);


                if (isNum1 && isNum2 && isNum3 && equation_textBox2D.Text.Length > 0 && x0_textBox.Text.Length > 0 && from_diffEquation_textBox.Text.Length > 0 && to_diffEquation_textBox.Text.Length > 0 && initialTextBoxFilled == true && conditionsDataIsCorrect == true && equationIsCorrect == true)
                    makePlot_diffEquation_button.Enabled = true;

                if (!isNum1 || !isNum2 || !isNum3 || equation_textBox2D.Text.Length == 0 || x0_textBox.Text.Length == 0 || from_diffEquation_textBox.Text.Length == 0 || to_diffEquation_textBox.Text.Length == 0 || initialTextBoxFilled == false || conditionsDataIsCorrect == false || equationIsCorrect == false)
                    makePlot_diffEquation_button.Enabled = false;

                if (equation_textBox2D.Text.Length > 0 || x0_textBox.Text.Length > 0 || from_diffEquation_textBox.Text.Length > 0 || to_diffEquation_textBox.Text.Length > 0 || initialTextBoxFilled == true)
                    eraseData_diffEquation_button.Enabled = true;

                if (equation_textBox2D.Text.Length == 0 && x0_textBox.Text.Length == 0 && from_diffEquation_textBox.Text.Length == 0 && to_diffEquation_textBox.Text.Length == 0 && initialTextBoxFilled == false)
                    eraseData_diffEquation_button.Enabled = false;

            }

            else if (senderPageName == "diffEquation3D" && parameterDependence_radioButton.Checked)
            {
                double isNum;
                bool isNum_x0 = Double.TryParse(x0_textBox3D.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_varyFrom = Double.TryParse(vary3DFrom_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_varyTo = Double.TryParse(vary3DTo_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_varyStep = Double.TryParse(vary3DStep_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_displayFrom = Double.TryParse(display3DFrom_textBox.Text, out isNum);
                bool isNum_displayTo = Double.TryParse(display3DTo_textBox.Text, out isNum);

                bool initialTextBoxFilled = true;
                bool conditionsDataIsCorrect = true;


                if (initialConditionsTextBoxes3DVaryParameter != null)
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes3DVaryParameter)
                    {
                        bool currTextBoxDataIsCorrect = true;
                        if (textbox.Text == "")
                        {
                            initialTextBoxFilled = false;
                        }
                        else
                            currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }
                bool equationIsCorrect = CheckFormulaCorrectness(equation_textBox3D);

                if (independentVariables3D != null && independentVariables3D.Count == 1 && dEquation3DVaryParam.amountOfVariablesToBeDefined > 0 && isNum_x0 && isNum_varyFrom && isNum_varyTo && isNum_varyStep && isNum_displayFrom && isNum_displayTo && equation_textBox3D.Text.Length > 0
                    && x0_textBox3D.Text.Length > 0 && vary3DFrom_textBox.Text.Length > 0 && vary3DTo_textBox.Text.Length > 0
                    && vary3DStep_textBox.Text.Length > 0 && display3DFrom_textBox.Text.Length > 0 && display3DTo_textBox.Text.Length > 0
                    && initialTextBoxFilled == true && conditionsDataIsCorrect == true && equationIsCorrect == true)

                    makePlot_diffEquation_button3D.Enabled = true;

                else if (independentVariables3D != null && (independentVariables3D.Count != 1 || dEquation3DVaryParam.amountOfVariablesToBeDefined <= 0 || !isNum_x0 || !isNum_varyFrom || !isNum_varyTo || !isNum_varyStep || !isNum_displayFrom || !isNum_displayTo
                    || equation_textBox3D.Text.Length == 0 || x0_textBox3D.Text.Length == 0 || vary3DFrom_textBox.Text.Length == 0
                    || vary3DTo_textBox.Text.Length == 0 || vary3DStep_textBox.Text.Length == 0 || display3DFrom_textBox.Text.Length == 0
                    || display3DTo_textBox.Text.Length == 0 || initialTextBoxFilled == false || conditionsDataIsCorrect == false || equationIsCorrect == false))

                    makePlot_diffEquation_button3D.Enabled = false;

                if (equation_textBox3D.Text.Length > 0 || x0_textBox3D.Text.Length > 0 || vary3DFrom_textBox.Text.Length > 0 || vary3DTo_textBox.Text.Length > 0
                    || vary3DStep_textBox.Text.Length > 0 || display3DFrom_textBox.Text.Length > 0 || display3DTo_textBox.Text.Length > 0 || initialTextBoxFilled == true)
                    eraseData_diffEquation_button3D.Enabled = true;

                else if (equation_textBox3D.Text.Length == 0 && x0_textBox3D.Text.Length == 0 && vary3DFrom_textBox.Text.Length == 0
                    && vary3DTo_textBox.Text.Length == 0 && vary3DStep_textBox.Text.Length == 0 && display3DFrom_textBox.Text.Length == 0
                    && display3DTo_textBox.Text.Length == 0 && initialTextBoxFilled == false)
                    eraseData_diffEquation_button3D.Enabled = false;

            }


            else if (senderPageName == "diffEquation3D" && initialValueDependence_radioButton.Checked)
            {
                double isNum;
                bool isNum_varyFrom = Double.TryParse(vary3DFrom_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_varyTo = Double.TryParse(vary3DTo_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_varyStep = Double.TryParse(vary3DStep_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_displayFrom = Double.TryParse(display3DFrom_textBox.Text, out isNum);
                bool isNum_displayTo = Double.TryParse(display3DTo_textBox.Text, out isNum);

                bool initialTextBoxFilled = true;
                bool conditionsDataIsCorrect = true;


                if (initialConditionsTextBoxes3DVaryInitValue != null)
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes3DVaryInitValue)
                    {
                        bool currTextBoxDataIsCorrect = true;
                        if (textbox.Text == "")
                        {
                            initialTextBoxFilled = false;
                        }
                        else
                            currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }

                bool equationIsCorrect = CheckFormulaCorrectness(equation_textBox3D);


                if (independentVariables3D != null && independentVariables3D.Count == 0 && dEquation3DInitialValue.amountOfVariablesToBeDefined > 0 && isNum_varyFrom && isNum_varyTo && isNum_varyStep && isNum_displayFrom && isNum_displayTo && equation_textBox3D.Text.Length > 0
                    && vary3DFrom_textBox.Text.Length > 0 && vary3DTo_textBox.Text.Length > 0
                    && vary3DStep_textBox.Text.Length > 0 && display3DFrom_textBox.Text.Length > 0 && display3DTo_textBox.Text.Length > 0 
                    && initialTextBoxFilled == true && conditionsDataIsCorrect==true && equationIsCorrect == true)

                    makePlot_diffEquation_button3D.Enabled = true;

                else if (independentVariables3D != null && (independentVariables3D.Count != 0 || dEquation3DInitialValue.amountOfVariablesToBeDefined < 1 || !isNum_varyFrom || !isNum_varyTo || !isNum_varyStep || !isNum_displayFrom || !isNum_displayTo
                    || equation_textBox3D.Text.Length == 0 || vary3DFrom_textBox.Text.Length == 0
                    || vary3DTo_textBox.Text.Length == 0 || vary3DStep_textBox.Text.Length == 0 || display3DFrom_textBox.Text.Length == 0
                    || display3DTo_textBox.Text.Length == 0 || initialTextBoxFilled == false || conditionsDataIsCorrect == false || equationIsCorrect == false))

                    makePlot_diffEquation_button3D.Enabled = false;

                if (equation_textBox3D.Text.Length > 0 || vary3DFrom_textBox.Text.Length > 0 || vary3DTo_textBox.Text.Length > 0
                    || vary3DStep_textBox.Text.Length > 0 || display3DFrom_textBox.Text.Length > 0 || display3DTo_textBox.Text.Length > 0 || initialTextBoxFilled == true)
                    eraseData_diffEquation_button3D.Enabled = true;

                else if (equation_textBox3D.Text.Length == 0 && vary3DFrom_textBox.Text.Length == 0
                    && vary3DTo_textBox.Text.Length == 0 && vary3DStep_textBox.Text.Length == 0 && display3DFrom_textBox.Text.Length == 0
                    && display3DTo_textBox.Text.Length == 0 && initialTextBoxFilled == false)
                    eraseData_diffEquation_button3D.Enabled = false;

            }

            else if (senderPageName == "simpleFunction2D") 
            {
                double isNum;
                bool isNum2 = Double.TryParse(from_simpleFunction_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum3 = Double.TryParse(to_simpleFunction_textBox.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool formulaIsCorrect = CheckFormulaCorrectness(function_textBox);

                

                if (isNum2 && isNum3 && function_textBox.Text.Length > 0 && from_simpleFunction_textBox.Text.Length > 0 && to_simpleFunction_textBox.Text.Length > 0 && formulaIsCorrect == true)
                    makePlot_simpleFunction_button.Enabled = true;

                if (!isNum2 || !isNum3 || function_textBox.Text.Length == 0 || from_simpleFunction_textBox.Text.Length == 0 || to_simpleFunction_textBox.Text.Length == 0 || formulaIsCorrect == false)
                    makePlot_simpleFunction_button.Enabled = false;

                if (function_textBox.Text.Length > 0 || from_simpleFunction_textBox.Text.Length > 0 || to_simpleFunction_textBox.Text.Length > 0)
                    eraseData_simpleFunction_button.Enabled = true;

                if (function_textBox.Text.Length == 0 && from_simpleFunction_textBox.Text.Length == 0 && to_simpleFunction_textBox.Text.Length == 0)
                    eraseData_simpleFunction_button.Enabled = false;

            }

            else if (senderPageName == "simpleFunction3D")
            {
                double isNum;
                bool isNum_xFrom = Double.TryParse(x_from_simpleFunction_textBox3D.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_xTo = Double.TryParse(x_to_simpleFunction_textBox3D.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_yFrom = Double.TryParse(y_from_simpleFunction_textBox3D.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool isNum_yTo = Double.TryParse(y_to_simpleFunction_textBox3D.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out isNum);
                bool formulaIsCorrect = CheckFormulaCorrectness(function_textBox3D);


                if (isNum_xFrom && isNum_xTo && isNum_yFrom && isNum_yTo && function_textBox3D.Text.Length > 0 
                    && x_from_simpleFunction_textBox3D.Text.Length > 0 && x_to_simpleFunction_textBox3D.Text.Length > 0
                    && y_from_simpleFunction_textBox3D.Text.Length > 0 && y_to_simpleFunction_textBox3D.Text.Length > 0
                    && formulaIsCorrect == true)
                    makePlot_simpleFunction_button3D.Enabled = true;

                if (!isNum_xFrom || !isNum_xTo || !isNum_yFrom || !isNum_yTo || function_textBox3D.Text.Length == 0 
                    || x_from_simpleFunction_textBox3D.Text.Length == 0 || x_to_simpleFunction_textBox3D.Text.Length == 0
                    || y_from_simpleFunction_textBox3D.Text.Length == 0 || y_to_simpleFunction_textBox3D.Text.Length == 0
                    || formulaIsCorrect == false)
                    makePlot_simpleFunction_button3D.Enabled = false;

                if (function_textBox3D.Text.Length > 0 || x_from_simpleFunction_textBox3D.Text.Length > 0 
                    || x_to_simpleFunction_textBox3D.Text.Length > 0 || y_from_simpleFunction_textBox3D.Text.Length > 0
                    || y_to_simpleFunction_textBox3D.Text.Length > 0)
                    eraseData_simpleFunction_button3D.Enabled = true;

                if (function_textBox3D.Text.Length == 0 && x_from_simpleFunction_textBox3D.Text.Length == 0
                    && x_to_simpleFunction_textBox3D.Text.Length == 0 && y_from_simpleFunction_textBox3D.Text.Length == 0
                    && y_to_simpleFunction_textBox3D.Text.Length == 0)
                    eraseData_simpleFunction_button3D.Enabled = false;

            }


        }
        #endregion

        private void MakeNewLabelsAndTextBoxes(TextBox equation_textBox, List<char> properFunctionString, List<List<char>> variablesInTheRightPart, TabPage tabPage,
            List<int>numberWithoutMinusInBrackets, ref List<char> independentVariables, ref List<string> variablesStr, 
            List<int> numbersAfterMinusInBrackets,ref int amountOfVariablesToBeDefined, ref TextBox[] initialConditionsTextBoxes, ref string[] dublicateInitialConditionsTextBoxesText)
        {
            if (equation_textBox == equation_textBox2D)
                dEquation2D.GetInitialDataForDiffEquation(equation_textBox, properFunctionString, variablesInTheRightPart, ref numberWithoutMinusInBrackets, ref independentVariables,
                ref variablesStr, ref numbersAfterMinusInBrackets, ref amountOfVariablesToBeDefined);

            else if (parameterDependence_radioButton1.Checked)
                dEquation3DVaryParam.GetInitialDataForDiffEquation(equation_textBox, properFunctionString, variablesInTheRightPart, ref numberWithoutMinusInBrackets, ref independentVariables,
                ref variablesStr, ref numbersAfterMinusInBrackets, ref amountOfVariablesToBeDefined);

            else if (initialValueDependence_radioButton1.Checked)
                dEquation3DInitialValue.GetInitialDataForDiffEquation(equation_textBox, properFunctionString, variablesInTheRightPart, ref numberWithoutMinusInBrackets, ref independentVariables,
                    ref variablesStr, ref numbersAfterMinusInBrackets, ref amountOfVariablesToBeDefined);

            // создание Label и Textbox-элементов
            System.Drawing.Point x0_textBox_DefLocation_local = new System.Drawing.Point(0, 0);
            System.Drawing.Point x0_label_DefLocation_local = new System.Drawing.Point(0, 0);

            if (equation_textBox == equation_textBox2D)
            {
                x0_textBox_DefLocation_local = x0_textBox_DefLocation;
                x0_label_DefLocation_local = x0_label_DefLocation;
            }
            else if (equation_textBox == equation_textBox3D)
            {
                 x0_textBox_DefLocation_local = x0_textBox_DefLocation3D;
                 x0_label_DefLocation_local = x0_label_DefLocation3D;

            }

            //удаляем предыдущие textBox и label
            if (initialConditionsTextBoxes != null)
            {
                for (int i = 1; i < initialConditionsTextBoxes.Length; i++)//первые label и textBox оставляем
                {
                    tabPage.Controls.Remove(initialConditionsTextBoxes[i]);
                    tabPage.Controls.Remove(initialConditionsLabels[i]);
                }
            }

            int yLocationStep = 35;
            if (amountOfVariablesToBeDefined > 0)
            { 
            initialConditionsTextBoxes = new TextBox[amountOfVariablesToBeDefined];
            initialConditionsLabels = new System.Windows.Forms.Label[amountOfVariablesToBeDefined];

            if (equation_textBox == equation_textBox2D)
            {
                initialConditionsTextBoxes[0] = x0_textBox;
                initialConditionsLabels[0] = x0_label;
            }
            else if (equation_textBox == equation_textBox3D)
            {
                initialConditionsTextBoxes[0] = x0_textBox3D;
                initialConditionsLabels[0] = x0_label3D;
            }
           
            for (int i = 0; i < amountOfVariablesToBeDefined - 1; i++)
            {
                System.Windows.Forms.Label newLabel = new System.Windows.Forms.Label();
                newLabel.Text = String.Format("x[{0}]=", (i + 1).ToString());
                newLabel.Location = new System.Drawing.Point(x0_label_DefLocation_local.X, x0_label_DefLocation_local.Y + yLocationStep);
                newLabel.Size = new Size(36,13);
                newLabel.Name = String.Format("x{0}_label", (i + 1).ToString());

                TextBox newTextBox = new TextBox();
                newTextBox.Location = new System.Drawing.Point(x0_textBox_DefLocation_local.X, x0_textBox_DefLocation_local.Y + yLocationStep);
                newTextBox.Name = String.Format("x{0}_textBox", (i+1).ToString());

                newTextBox.TextChanged += new EventHandler(x0_textBox_TextChanged);//!!!! 
                if (equation_textBox == equation_textBox2D) 
                    newTextBox.Leave += new EventHandler(x0_textBox_Leave); 
                else if (equation_textBox == equation_textBox3D)
                    newTextBox.Leave += new EventHandler(x0_textBox3D_Leave);

                    tabPage.Controls.Add(newLabel);
                tabPage.Controls.Add(newTextBox);
                initialConditionsTextBoxes[i + 1] = newTextBox;
                initialConditionsLabels[i + 1] = newLabel;

                yLocationStep += 35;
            }

            yLocationStep -= 35;


            //ИСПРАВИТЬ ПОЗИЦИИ И НАЗВАНИЯ КОНТРОЛОВ УНИВЕРС.!
            if (equation_textBox == equation_textBox2D)
            {
                from_diffEquation_label.Location = new System.Drawing.Point(from_diffEquation_label_DefLocation.X, from_diffEquation_label_DefLocation.Y + yLocationStep);
                from_diffEquation_textBox.Location = new System.Drawing.Point(from_diffEquation_textBox_DefLocation.X, from_diffEquation_textBox_DefLocation.Y + yLocationStep);

                to_diffEquation_label.Location = new System.Drawing.Point(to_diffEquation_label_DefLocation.X, to_diffEquation_label_DefLocation.Y + yLocationStep);
                to_diffEquation_textBox.Location = new System.Drawing.Point(to_diffEquation_textBox_DefLocation.X, to_diffEquation_textBox_DefLocation.Y + yLocationStep);

                color_diffEquation_label.Location = new System.Drawing.Point(color_diffEquation_label_DefLocation.X, color_diffEquation_label_DefLocation.Y + yLocationStep);
                color_diffEquation_button.Location = new System.Drawing.Point(color_diffEquation_button_DefLocation.X, color_diffEquation_button_DefLocation.Y + yLocationStep);
                markPoints_checkBox.Location = new System.Drawing.Point(markPoints_checkBox_DefLocation.X, markPoints_checkBox_DefLocation.Y +yLocationStep);


                    if (dublicateInitialConditionsTextBoxesText != null && initialConditionsTextBoxes != null)
                {
                    if (dublicateInitialConditionsTextBoxesText.Length <= initialConditionsTextBoxes.Length)
                    {
                        for (int i = 0; i < dublicateInitialConditionsTextBoxesText.Length; i++)
                            initialConditionsTextBoxes[i].Text = dublicateInitialConditionsTextBoxesText[i];
                    }
                }
            }
            if (equation_textBox == equation_textBox3D)
            {
                varyParameter_label.Location = new System.Drawing.Point(varyParameter_label_DefLocation3D.X, varyParameter_label_DefLocation3D.Y + yLocationStep);

                vary3DFrom_label.Location = new System.Drawing.Point(vary3DFrom_labelDefLocation3D.X, vary3DFrom_labelDefLocation3D.Y + yLocationStep);
                vary3DFrom_textBox.Location = new System.Drawing.Point(vary3DFrom_textBoxDefLocation3D.X, vary3DFrom_textBoxDefLocation3D.Y + yLocationStep);

                vary3DTo_label.Location = new System.Drawing.Point(vary3DTo_labelDefLocation3D.X, vary3DTo_labelDefLocation3D.Y + yLocationStep);
                vary3DTo_textBox.Location = new System.Drawing.Point(vary3DTo_textBoxDefLocation3D.X, vary3DTo_textBoxDefLocation3D.Y + yLocationStep);

                vary3DStep_label.Location = new System.Drawing.Point(vary3DStep_labelDefLocation3D.X, vary3DStep_labelDefLocation3D.Y + yLocationStep);
                vary3DStep_textBox.Location = new System.Drawing.Point(vary3DStep_textBoxDefLocation3D.X, vary3DStep_textBoxDefLocation3D.Y + yLocationStep);

                displayResults_label.Location = new System.Drawing.Point(displayResults_label_DefLocation3D.X, displayResults_label_DefLocation3D.Y + yLocationStep);

                display3DFrom_label.Location = new System.Drawing.Point(display3DFrom_labelDefLocation3D.X, display3DFrom_labelDefLocation3D.Y + yLocationStep);
                display3DFrom_textBox.Location = new System.Drawing.Point(display3DFrom_textBoxDefLocation3D.X, display3DFrom_textBoxDefLocation3D.Y + yLocationStep);

                display3DTo_label.Location = new System.Drawing.Point(display3DTo_labelDefLocation3D.X, display3DTo_labelDefLocation3D.Y + yLocationStep);
                display3DTo_textBox.Location = new System.Drawing.Point(display3DTo_textBoxDefLocation3D.X, display3DTo_textBoxDefLocation3D.Y + yLocationStep);

                    if (dublicateInitialConditionsTextBoxesText != null && initialConditionsTextBoxes != null)
                {
                    if (dublicateInitialConditionsTextBoxesText.Length <= initialConditionsTextBoxes.Length)
                    {
                        for (int i = 0; i < dublicateInitialConditionsTextBoxesText.Length; i++)
                            initialConditionsTextBoxes[i].Text = dublicateInitialConditionsTextBoxesText[i];
                    }
                }
            }
            }
        }


        private void eraseData_button_Click(object sender, EventArgs e)
        {
            if (sender == eraseData_diffEquation_button)
            {
                equation_textBox2D.Text = "";
                if (initialConditionsTextBoxes2D != null)
                {
                    for (int i = 1; i < initialConditionsTextBoxes2D.Length; i++)//первые label и textBox оставляем
                    {
                        differenceEquation_tabPage.Controls.Remove(initialConditionsTextBoxes2D[i]);
                        differenceEquation_tabPage.Controls.Remove(initialConditionsLabels[i]);
                    }
                }
                x0_textBox.Text = "";

                from_diffEquation_textBox.Text = "";
                from_diffEquation_textBox.Location = from_diffEquation_textBox_DefLocation;
                from_diffEquation_label.Location = from_diffEquation_label_DefLocation;

                to_diffEquation_textBox.Text = "";
                to_diffEquation_label.Location = to_diffEquation_label_DefLocation;
                to_diffEquation_textBox.Location = to_diffEquation_textBox_DefLocation;

                color_diffEquation_label.Location = color_diffEquation_label_DefLocation;
                color_diffEquation_button.Location = color_diffEquation_button_DefLocation;
                colorDialog1.Color = System.Drawing.Color.Black;
                color = System.Drawing.Color.Black;
                markPoints_checkBox.Location = markPoints_checkBox_DefLocation;
                markPoints_checkBox.Checked = false;
                incorrectData_label.Visible = false;
            }


            if( sender == eraseData_diffEquation_button3D)
            {
                if (parameterDependence_radioButton.Checked)
                {
                    if (initialConditionsTextBoxes3DVaryParameter != null)
                    {
                        for (int i = 1; i < initialConditionsTextBoxes3DVaryParameter.Length; i++)//первые label и textBox оставляем
                        {
                            differenceEquation3D_tabPage.Controls.Remove(initialConditionsTextBoxes3DVaryParameter[i]);
                            differenceEquation3D_tabPage.Controls.Remove(initialConditionsLabels[i]);
                        }
                    }
                }
                else
                {
                    if (initialConditionsTextBoxes3DVaryParameter != null)
                    {
                        for (int i = 1; i < initialConditionsTextBoxes3DVaryParameter.Length; i++)//первые label и textBox оставляем
                        {
                            differenceEquation3D_tabPage.Controls.Remove(initialConditionsTextBoxes3DVaryParameter[i]);
                            differenceEquation3D_tabPage.Controls.Remove(initialConditionsLabels[i]);
                        }
                    }
                }
                if (parameterDependence_radioButton.Checked)
                    x0_textBox3D.Text = "";

                equation_textBox3D.Text = "";//эта строка кода должна выполняться после удаления текстбоксов в цикле выше, т.к. иначе сработает событие изменения уравнения и доступ к ним потеряется

                varyParameter_label.Location = varyParameter_label_DefLocation3D;

                vary3DFrom_textBox.Text = "";
                vary3DFrom_textBox.Location = vary3DFrom_textBoxDefLocation3D ;
                vary3DFrom_label.Location = vary3DFrom_labelDefLocation3D;

                vary3DTo_textBox.Text = "";
                vary3DTo_label.Location = vary3DTo_labelDefLocation3D;
                vary3DTo_textBox.Location = vary3DTo_textBoxDefLocation3D;

                vary3DStep_textBox.Text = "";
                vary3DStep_label.Location = vary3DStep_labelDefLocation3D;
                vary3DStep_textBox.Location = vary3DStep_textBoxDefLocation3D;

                displayResults_label.Location = displayResults_label_DefLocation3D;

                display3DFrom_textBox.Text = "";
                display3DFrom_textBox.Location = display3DFrom_textBoxDefLocation3D;
                display3DFrom_label.Location = display3DFrom_labelDefLocation3D;

                display3DTo_textBox.Text = "";
                display3DTo_label.Location = display3DTo_labelDefLocation3D;
                display3DTo_textBox.Location = display3DTo_textBoxDefLocation3D;
                incorrectData3D_label.Visible = false;
            }

            if(sender == eraseData_simpleFunction_button)
            {
                function_textBox.Text = "";
                from_simpleFunction_textBox.Text = "";
                to_simpleFunction_textBox.Text = "";
                colorDialog1.Color = System.Drawing.Color.Black;
                color = System.Drawing.Color.Black;
                incorrectDataSimpleF_label.Visible = false;
            }

            if (sender == eraseData_simpleFunction_button3D)
            {
                function_textBox3D.Text = "";
                x_from_simpleFunction_textBox3D.Text = "";
                x_to_simpleFunction_textBox3D.Text = "";
                y_from_simpleFunction_textBox3D.Text = "";
                y_to_simpleFunction_textBox3D.Text = "";
                incorrectData3DSimpleF_label.Visible = false;
            }

        }

        private void erasePlot_button_Click(object sender, EventArgs e)
        {            
            pane.CurveList.Clear();
            // Обновляем график
            zedGraphControl1.Invalidate();
            erasePlot_button.Enabled = false;
        }

        private void equation_textBox_Leave(object sender, EventArgs e)
        {
            if (equation_textBox2D.Text != "")
            {
                dublicateInitialConditionsTextBoxesText2D = new string[dEquation2D.amountOfVariablesToBeDefined];
                if (initialConditionsTextBoxes2D != null)
                    for (int i = 0; i < initialConditionsTextBoxes2D.Length; i++)
                    {
                        dublicateInitialConditionsTextBoxesText2D[i] = initialConditionsTextBoxes2D[i].Text;
                    }

                dEquation2D = new diffEquation2D()
                {
                    properFunctionString = new List<char>(),
                    variablesInRightPart = new List<List<char>>(),
                    numbersWithoutMinusInBrackets = new List<int>(),
                    independentVariables = new List<char>(),
                    variablesStr = new List<string>(),
                    numbersAfterMinusInBrackets = new List<int>(),
                    amountOfVariablesToBeDefined = new Int32(),
                    functionContainsParamN = new bool()
                };
                MakeNewLabelsAndTextBoxes(equation_textBox2D, dEquation2D.properFunctionString, dEquation2D.variablesInRightPart, differenceEquation_tabPage,
                    dEquation2D.numbersWithoutMinusInBrackets, ref dEquation2D.independentVariables, ref dEquation2D.variablesStr, 
                    dEquation2D.numbersAfterMinusInBrackets, ref dEquation2D.amountOfVariablesToBeDefined, ref initialConditionsTextBoxes2D, ref dublicateInitialConditionsTextBoxesText2D);
                //bool syntaxOk = dEquation2D.GetFunction().checkSyntax();
                bool formulaIsCorrect = true;
                formulaIsCorrect = CheckFormulaCorrectness(equation_textBox2D);
                if (formulaIsCorrect == false)
                    incorrectData_label.Visible = true;
                else
                    incorrectData_label.Visible = false;
            }
        }

        private void makePlot_simpleFunction_button_Click(object sender, EventArgs e)
        {
            SimpleFunction2D simpleFunction2D = new SimpleFunction2D();
            Function function = simpleFunction2D.GetFunction(function_textBox.Text);

            double from = Double.Parse(from_simpleFunction_textBox.Text);
            double to = Double.Parse(to_simpleFunction_textBox.Text);
            double step = 0.01;

            double[] arguments;
            simpleFunction2D.GetAndStoreTheArgsValues(out arguments, from, to, step);

            double[] results = simpleFunction2D.CalculateResults(arguments, function);
            Draw(arguments, results, step, "f(x)=" + function_textBox.Text, SymbolType.None);
        }



     


        //3D
        #region
        private void makePlot_diffEquation_button3D_Click(object sender, EventArgs e)
        {
            display3DFrom = Int32.Parse(display3DFrom_textBox.Text);
            display3DTo = Int32.Parse(display3DTo_textBox.Text);
            vary3DFrom = Double.Parse(vary3DFrom_textBox.Text, CultureInfo.GetCultureInfo("en-US"));
            vary3DTo = Double.Parse(vary3DTo_textBox.Text, CultureInfo.GetCultureInfo("en-US"));
            vary3DStep = Double.Parse(vary3DStep_textBox.Text, CultureInfo.GetCultureInfo("en-US"));

            // Если нужно построить график зависимости результатов в каждый момент времени от значения параметра
            if (parameterDependence_radioButton.Checked)
            {
                dublicateInitialConditionsTextBoxesText3DVaryParameter = new string[dEquation3DVaryParam.amountOfVariablesToBeDefined];
                if (initialConditionsTextBoxes3DVaryParameter != null)
                    for (int i = 0; i < initialConditionsTextBoxes3DVaryParameter.Length; i++)
                {
                    dublicateInitialConditionsTextBoxesText3DVaryParameter[i] = initialConditionsTextBoxes3DVaryParameter[i].Text;
                }

                initialConditionsTextBoxes3DVaryParameter[0] = x0_textBox3D;

            }

            else if (initialValueDependence_radioButton.Checked)
            {
                dublicateInitialConditionsTextBoxesText3DVaryInitValue = new string[dEquation3DInitialValue.amountOfVariablesToBeDefined];
                if (initialConditionsTextBoxes3DVaryInitValue != null)
                    for (int i = 1; i < initialConditionsTextBoxes3DVaryInitValue.Length; i++) 
                {
                    dublicateInitialConditionsTextBoxesText3DVaryInitValue[i] = initialConditionsTextBoxes3DVaryInitValue[i].Text;
                }
            }
            InitRenderer();
        }

        private void InitRenderer()
        {
            // Create a range for the graph generation
            Range range = new Range(-150, 150);
            int steps = 50;


            // Build a nice surface to display with cool alpha colors 
            // (alpha 0.8 for surface color and 0.5 for wireframe)
            Shape surface = Builder.buildOrthonomal(new myOrthonormalGrid(range, steps, range, steps), new MyMapper());
            surface.ColorMapper = new ColorMapper(new ColorMapRainbow(), surface.Bounds.zmin, surface.Bounds.zmax, new nzy3D.Colors.Color(1, 1, 1, 0.8));
            surface.FaceDisplayed = true;
            surface.WireframeDisplayed = true;
            surface.WireframeColor = nzy3D.Colors.Color.CYAN;
            surface.WireframeColor.mul(new nzy3D.Colors.Color(1, 1, 1, 0.5));

            // Create the chart and embed the surface within
            nzy3D.Chart.Chart chart = new nzy3D.Chart.Chart(myRenderer3D, Quality.Nicest);
            chart.Scene.Graph.Add(surface);
            axeLayout = chart.AxeLayout;


            if (!simpleFunction3DMakingPlot)
            {
                axeLayout.XAxeLabel = "n";
                axeLayout.ZAxeLabel = "X";
                if (initialValueDependence_radioButton.Checked)
                {
                    axeLayout.YAxeLabel = "X[0]";
                }
                else
                {
                    axeLayout.YAxeLabel = dEquation3DVaryParam.independentVariables[0].ToString();
                }
            }
            else
            {
                axeLayout.XAxeLabel = "X";
                axeLayout.YAxeLabel = "Y";
                axeLayout.ZAxeLabel = "Z";
            }
            // All activated by default
            DisplayXTicks = true;
            DisplayXAxisLabel = true;
            DisplayYTicks = true;
            DisplayYAxisLabel = true;
            DisplayZTicks = true;
            DisplayZAxisLabel = true;
            DisplayTickLines = true;

            // Create a mouse control
            nzy3D.Chart.Controllers.Mouse.Camera.CameraMouseController mouse = new nzy3D.Chart.Controllers.Mouse.Camera.CameraMouseController();
            mouse.addControllerEventListener(myRenderer3D);
            chart.addController(mouse);

            // This is just to ensure code is reentrant (used when code is not called in Form_Load but another reentrant event)
            DisposeBackgroundThread();

            // Create a thread to control the camera based on mouse movements
            t = new nzy3D.Chart.Controllers.Thread.Camera.CameraThreadController();
            t.addControllerEventListener(myRenderer3D);
            mouse.addSlaveThreadController(t);
            chart.addController(t);
            t.Start();

            // Associate the chart with current control
            myRenderer3D.setView(chart.View);

            this.Refresh();
        }

        private void DisposeBackgroundThread()
        {
            if ((t != null))
            {
                t.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeBackgroundThread();
        }


        private bool _DisplayTickLines;
        public bool DisplayTickLines
        {
            get
            {
                return _DisplayTickLines;
            }
            set
            {
                _DisplayTickLines = value;
                if (axeLayout != null)
                {
                    axeLayout.TickLineDisplayed = value;
                }
            }
        }

        private bool _DisplayXTicks;
        public bool DisplayXTicks
        {
            get
            {
                return _DisplayXTicks;
            }
            set
            {
                _DisplayXTicks = value;
                if (axeLayout != null)
                {
                    axeLayout.XTickLabelDisplayed = value;
                }
            }
        }

        private bool _DisplayYTicks;
        public bool DisplayYTicks
        {
            get
            {
                return _DisplayYTicks;
            }
            set
            {
                _DisplayYTicks = value;
                if (axeLayout != null)
                {
                    axeLayout.YTickLabelDisplayed = value;
                }
            }
        }

        private bool _DisplayZTicks;
        public bool DisplayZTicks
        {
            get
            {
                return _DisplayZTicks;
            }
            set
            {
                _DisplayZTicks = value;
                if (axeLayout != null)
                {
                    axeLayout.ZTickLabelDisplayed = value;
                }
            }
        }

        private bool _DisplayXAxisLabel;
        public bool DisplayXAxisLabel
        {
            get
            {
                return _DisplayXAxisLabel;
            }
            set
            {
                _DisplayXAxisLabel = value;
                if (axeLayout != null)
                {
                    axeLayout.XAxeLabelDisplayed = value;
                }
            }
        }

        private bool _DisplayYAxisLabel;
        public bool DisplayYAxisLabel
        {
            get
            {
                return _DisplayYAxisLabel;
            }
            set
            {
                _DisplayYAxisLabel = value;
                if (axeLayout != null)
                {
                    axeLayout.YAxeLabelDisplayed = value;
                }
            }
        }

        private bool _DisplayZAxisLabel;
        public bool DisplayZAxisLabel
        {
            get
            {
                return _DisplayZAxisLabel;
            }
            set
            {
                _DisplayZAxisLabel = value;
                if (axeLayout != null)
                {
                    axeLayout.ZAxeLabelDisplayed = value;
                }
            }
        }
        #endregion

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(sender == parameterDependence_radioButton)
            {
                {                   
                    x0_textBox3D.Text = "";
                    x0_textBox3D.Enabled = true;
                    varyParameter_label.Text = "Vary parameter";
                    ReviseMakePlotButtonEnabling();
                }
            }
            if (sender == initialValueDependence_radioButton)
            {
                {                   
                    x0_textBox3D.Text = "Will be varied";
                    x0_textBox3D.Enabled = false;
                    varyParameter_label.Text = "Vary initial value";
                    ReviseMakePlotButtonEnabling();
                }
            }

        }

        private void equation_textBox3D_Leave(object sender, EventArgs e)
        {
            if (equation_textBox3D.Text != "")
            {
                if (parameterDependence_radioButton.Checked)
                {                  
                    dublicateInitialConditionsTextBoxesText3DVaryParameter = new string[dEquation3DVaryParam.amountOfVariablesToBeDefined];
                    if (initialConditionsTextBoxes3DVaryParameter != null)
                        for (int i = 0; i < initialConditionsTextBoxes3DVaryParameter.Length; i++)
                        {
                            dublicateInitialConditionsTextBoxesText3DVaryParameter[i] = initialConditionsTextBoxes3DVaryParameter[i].Text;
                        }
                    dEquation3DVaryParam = new DiffEquationVaryParameter()
                    {
                        properFunctionString = new List<char>(),
                        variablesInRightPart = new List<List<char>>(),
                        numbersWithoutMinusInBrackets = new List<int>(),
                        independentVariables = new List<char>(),
                        variablesStr = new List<string>(),
                        numbersAfterMinusInBrackets = new List<int>(),
                        amountOfVariablesToBeDefined = new Int32(),
                        functionContainsParamN = new bool()
                    };
                    MakeNewLabelsAndTextBoxes(equation_textBox3D, dEquation3DVaryParam.properFunctionString, dEquation3DVaryParam.variablesInRightPart, differenceEquation3D_tabPage,
                        dEquation3DVaryParam.numbersWithoutMinusInBrackets, ref dEquation3DVaryParam.independentVariables, ref dEquation3DVaryParam.variablesStr, 
                        dEquation3DVaryParam.numbersAfterMinusInBrackets, ref dEquation3DVaryParam.amountOfVariablesToBeDefined, ref initialConditionsTextBoxes3DVaryParameter,
                        ref dublicateInitialConditionsTextBoxesText3DVaryParameter);
                    bool formulaIsCorrect = true;
                    formulaIsCorrect = CheckFormulaCorrectness(equation_textBox3D);
                    if (formulaIsCorrect == false)
                        incorrectData3D_label.Visible = true;
                    else
                        incorrectData3D_label.Visible = false;
                }
                else
                {
                    dublicateInitialConditionsTextBoxesText3DVaryInitValue = new string[dEquation3DInitialValue.amountOfVariablesToBeDefined];
                    if (initialConditionsTextBoxes3DVaryInitValue != null)
                    {
                        for (int i = 1; i < initialConditionsTextBoxes3DVaryInitValue.Length; i++)
                        {
                            dublicateInitialConditionsTextBoxesText3DVaryInitValue[i] = initialConditionsTextBoxes3DVaryInitValue[i].Text;
                        }
                    }
                    dEquation3DInitialValue = new DiffEquationVaryInitialValue()
                    {
                        properFunctionString = new List<char>(),
                        variablesInRightPart = new List<List<char>>(),
                        numbersWithoutMinusInBrackets = new List<int>(),
                        independentVariables = new List<char>(),
                        variablesStr = new List<string>(),
                        numbersAfterMinusInBrackets = new List<int>(),
                        amountOfVariablesToBeDefined = new Int32(),
                        functionContainsParamN = new bool()
                    };

                    MakeNewLabelsAndTextBoxes(equation_textBox3D, dEquation3DInitialValue.properFunctionString, dEquation3DInitialValue.variablesInRightPart, differenceEquation3D_tabPage,
                        dEquation3DInitialValue.numbersWithoutMinusInBrackets, ref dEquation3DInitialValue.independentVariables, ref dEquation3DInitialValue.variablesStr, dEquation3DInitialValue.numbersAfterMinusInBrackets,
                        ref dEquation3DInitialValue.amountOfVariablesToBeDefined, ref initialConditionsTextBoxes3DVaryInitValue, ref dublicateInitialConditionsTextBoxesText3DVaryInitValue);
                    bool formulaIsCorrect = true;
                    formulaIsCorrect = CheckFormulaCorrectness(equation_textBox3D);
                    if (formulaIsCorrect == false)
                        incorrectData3D_label.Visible = true;
                    else
                        incorrectData3D_label.Visible = false;
                }
                ReviseMakePlotButtonEnabling();
            }
        }

        private void ReviseMakePlotButtonEnabling()
        {
            makePlot_diffEquation_button3D.Enabled = false;
            if (equation_textBox3D.Text != "")
            {
                if (parameterDependence_radioButton.Checked)
                {
                    dEquation3DVaryParam = new DiffEquationVaryParameter()
                    {
                        properFunctionString = new List<char>(),
                        variablesInRightPart = new List<List<char>>(),
                        numbersWithoutMinusInBrackets = new List<int>(),
                        independentVariables = new List<char>(),
                        variablesStr = new List<string>(),
                        numbersAfterMinusInBrackets = new List<int>(),
                        amountOfVariablesToBeDefined = new Int32(),
                        functionContainsParamN = new bool()
                    };

                    dEquation3DVaryParam.GetInitialDataForDiffEquation(equation_textBox3D, dEquation3DVaryParam.properFunctionString, dEquation3DVaryParam.variablesInRightPart,
                    ref dEquation3DVaryParam.numbersWithoutMinusInBrackets, ref dEquation3DVaryParam.independentVariables, ref dEquation3DVaryParam.variablesStr, ref dEquation3DVaryParam.numbersAfterMinusInBrackets, ref dEquation3DVaryParam.amountOfVariablesToBeDefined);
                    bool formulaIsCorrect = CheckFormulaCorrectness(equation_textBox3D);
                    if (dEquation3DVaryParam.independentVariables.Count == 1 && dEquation3DVaryParam.amountOfVariablesToBeDefined > 0
                        && formulaIsCorrect)
                        makePlot_diffEquation_button3D.Enabled = true;
                    else
                    {
                        makePlot_diffEquation_button3D.Enabled = false;
                    }
                }

                if (initialValueDependence_radioButton.Checked)
                {
                    dEquation3DInitialValue = new DiffEquationVaryInitialValue()
                    {
                        properFunctionString = new List<char>(),
                        variablesInRightPart = new List<List<char>>(),
                        numbersWithoutMinusInBrackets = new List<int>(),
                        independentVariables = new List<char>(),
                        variablesStr = new List<string>(),
                        numbersAfterMinusInBrackets = new List<int>(),
                        amountOfVariablesToBeDefined = new Int32(),
                        functionContainsParamN = new bool()
                    };
                    dEquation3DInitialValue.GetInitialDataForDiffEquation(equation_textBox3D, dEquation3DInitialValue.properFunctionString, dEquation3DInitialValue.variablesInRightPart,
                    ref dEquation3DInitialValue.numbersWithoutMinusInBrackets, ref dEquation3DInitialValue.independentVariables, ref dEquation3DInitialValue.variablesStr, ref dEquation3DInitialValue.numbersAfterMinusInBrackets, ref dEquation3DInitialValue.amountOfVariablesToBeDefined);
                    bool formulaIsCorrect = CheckFormulaCorrectness(equation_textBox3D);

                    if (dEquation3DInitialValue.independentVariables.Count == 0 && dEquation3DInitialValue.amountOfVariablesToBeDefined > 0
                        && formulaIsCorrect)
                        makePlot_diffEquation_button3D.Enabled = true;
                    else
                    {
                        makePlot_diffEquation_button3D.Enabled = false;
                    }
                }
            }
        }

        private bool CheckFormulaCorrectness(TextBox equation_textBox)
        {
            bool isCorrect = true;
            if (equation_textBox != null)
            {
                int countOpen = 0;
                int countClose = 0;

                if (equation_textBox.Text.Contains('['))
                {
                    for (int i = 0; i < equation_textBox.Text.Length; i++)
                    {
                        if (equation_textBox.Text.ElementAt(i) == '[')
                            countOpen++;
                        if (equation_textBox.Text.ElementAt(i) == ']')
                            countClose++;
                    }
                    if (countOpen != countClose)
                        isCorrect = false;
                }

                if (equation_textBox.Text.Contains('('))
                {
                    countOpen = 0;
                    countClose = 0;
                    for (int i = 0; i < equation_textBox.Text.Length; i++)
                    {
                        if (equation_textBox.Text.ElementAt(i) == '(')
                            countOpen++;
                        if (equation_textBox.Text.ElementAt(i) == ')')
                            countClose++;
                    }
                    if (countOpen != countClose)
                        isCorrect = false;
                }
            }
            return isCorrect;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void to_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {

        }

        private void from_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1)
                splitContainer2.Panel2.Refresh();
        }

        private void makePlot_simpleFunction_button3D_Click(object sender, EventArgs e)
        {
            simpleFunction3DMakingPlot = true;
            function_textBox3DText = function_textBox3D.Text;

            step_simleFunction3D = 0.5;
            x_from_simleFunction3D = Double.Parse(x_from_simpleFunction_textBox3D.Text, CultureInfo.GetCultureInfo("en-US"));
            x_to_simleFunction3D = Double.Parse(x_to_simpleFunction_textBox3D.Text, CultureInfo.GetCultureInfo("en-US"));
            y_from_simleFunction3D = Double.Parse(y_from_simpleFunction_textBox3D.Text, CultureInfo.GetCultureInfo("en-US"));
            y_to_simleFunction3D = Double.Parse(y_to_simpleFunction_textBox3D.Text, CultureInfo.GetCultureInfo("en-US"));

            InitRenderer();
            simpleFunction3DMakingPlot = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void from_x_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("simpleFunction3D");
        }

        private void to_x_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("simpleFunction3D");
        }

        private void from_y_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("simpleFunction3D");
        }

        private void to_y_diffEquation_textBox3D_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("simpleFunction3D");
        }

        private void from_vary3D_textBox_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("diffEquation3D");
        }

        private void to_vary3D_textBox_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("diffEquation3D");
        }

        private void step_vary3D_textBox_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("diffEquation3D");
        }

        private void from_display3D_textBox_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("diffEquation3D");
        }

        private void to_display3D_textBox_TextChanged(object sender, EventArgs e)
        {
            ControlButtons("diffEquation3D");
        }

        private void differenceEquation_tabPage_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void x0_textBox3D_Leave(object sender, EventArgs e)
        {
            bool conditionsDataIsCorrect = true;
            if (parameterDependence_radioButton.Checked)
            {
                if (initialConditionsTextBoxes3DVaryParameter != null)
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes3DVaryParameter)
                    {
                        bool currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }
            }
            else
            {
                if (initialConditionsTextBoxes3DVaryInitValue != null)
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes3DVaryInitValue)
                    {
                        bool currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }
            }

            if (conditionsDataIsCorrect)
               incorrectData3D_label.Visible = false;
            else
                incorrectData3D_label.Visible = true;
        }

        private void x0_textBox_Leave(object sender, EventArgs e)
        {
            bool conditionsDataIsCorrect = true;
            if (initialConditionsTextBoxes2D != null) 
                {
                    foreach (TextBox textbox in initialConditionsTextBoxes2D)
                    {
                        bool currTextBoxDataIsCorrect = CheckFormulaCorrectness(textbox);
                        if (currTextBoxDataIsCorrect == false)
                            conditionsDataIsCorrect = false;
                    }
                }
            
            if (conditionsDataIsCorrect)
                incorrectData_label.Visible = false;
            else
                incorrectData_label.Visible = true;
        }

        private void function_textBox_Leave(object sender, EventArgs e)
        {
            bool functionIsCorrect = CheckFormulaCorrectness(function_textBox);

            if (functionIsCorrect)
                incorrectDataSimpleF_label.Visible = false;
            else
                incorrectDataSimpleF_label.Visible = true;
        }

        private void function_textBox3D_Leave(object sender, EventArgs e)
        {
            bool functionIsCorrect = CheckFormulaCorrectness(function_textBox3D);

            if (functionIsCorrect)
                incorrectData3DSimpleF_label.Visible = false;
            else
                incorrectData3DSimpleF_label.Visible = true;
        }
    }
}
