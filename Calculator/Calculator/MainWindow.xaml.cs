using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public enum Operations
    {
        Division,
        Multiplication,
        Minus,
        Plus

    }
    public partial class MainWindow : Window
    {
        string calculations;

        float temp = 0;

        bool cleared=false;

        string operation = "";

        string output = "";
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            DivisionBtn.Content = "\u00F7";
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;

            if (cleared)
            {
                output = string.Empty;

                calculations = output;

                cleared = false;
            }

            switch (name)
            {
                case "ZeroBtn":
                    output += "0";
                    OutputTextBlock.Text = output;
                    break;
                case "OneBtn":
                    output += "1";
                    OutputTextBlock.Text = output;
                    break;
                case "TwoBtn":
                    output += "2";
                    OutputTextBlock.Text = output;
                    break;
                case "ThreeBtn":
                    output += "3";
                    OutputTextBlock.Text = output;
                    break;
                case "FourBtn":
                    output += "4";
                    OutputTextBlock.Text = output;
                    break;
                case "FiveBtn":
                    output += "5";
                    OutputTextBlock.Text = output;
                    break;
                case "SixBtn":
                    output += "6";
                    OutputTextBlock.Text = output;
                    break;
                case "SevenBtn":
                    output += "7";
                    OutputTextBlock.Text = output;
                    break;
                case "EightBtn":
                    output += "8";
                    OutputTextBlock.Text = output;
                    break;
                case "NineBtn":
                    output += "9";
                    OutputTextBlock.Text = output;
                    break;
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            output = string.Empty;
            temp = 0;

            OutputTextBlock.Text = output;

            calculations = string.Empty;

            CalculationsTextBlock.Text = calculations;
        }

        private void DivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + DivisionBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Division.ToString();
            }
        }

        private void MultiplicationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + MultiplicationBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Multiplication.ToString();
            }
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            if(output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + MinusBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Minus.ToString();
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + PlusBtn.Content + " ";

                output = string.Empty;

                
                operation = Operations.Plus.ToString();
            }
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            Enum.TryParse(operation, out Operations myStatus);


            CalculationsTextBlock.Text += output.ToString();

            switch (myStatus)
            {
                case Operations.Minus:
                    float outputTemp = temp - float.Parse(output);
                    output = outputTemp.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Division:
                    float outputTemp2 = temp / float.Parse(output);
                    output = outputTemp2.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Plus:
                    float outputTemp3 = temp + float.Parse(output);
                    output = outputTemp3.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Multiplication:
                    float outputTemp4 = temp * float.Parse(output);
                    output = outputTemp4.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                default:
                    break;


            }
        }

        private void NegateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                output = (float.Parse(output) * -1).ToString();
                OutputTextBlock.Text = output;
            }
        }

        private void PointBtn_Click(object sender, RoutedEventArgs e)
        {
            if (cleared)
            {
                output = string.Empty;

                calculations = output;

                CalculationsTextBlock.Text = calculations;

                cleared = false;
            }

            if (output == string.Empty)
            {
                output = "0.";
            }
            else
            {
                output = (float.Parse(output)).ToString() + ".";
            }

            OutputTextBlock.Text = output;

        }
    }
}
