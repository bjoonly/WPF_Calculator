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

namespace Calculatr
{

    public partial class MainWindow : Window
    {

        private decimal result = 0;
        private string operand = "";
        private string operation = "";
        private int lastOperationIndex = 0;
        bool Clear = true;
        bool isEqual = false;
        public MainWindow()
        {
            InitializeComponent();
            textBoxNumber.Text = result.ToString();

        }

        private void AllNumbers_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();
            if (Clear || (operand[0]=='0'  && operand.Length==1))
            {
                textBoxNumber.Clear();
                operand = "";

            }
            if (s == "0" && operand.Length==1 && operand[0]=='0')
            {
                textBoxNumber.Text = "0";
                operand = s;
            }
            else
            {
                
                operand += s;
                textBoxNumber.Text += s;
            }
           
            Clear = false;
           
        }
        private void Calculate()
        {
            switch (operation)
            {
                case "+":
                    result += Convert.ToDecimal(operand);
                    break;
                case "-":
                    result -= Convert.ToDecimal(operand);
                    break;
                case "/":
                    if (Convert.ToDecimal(operand) == 0)
                    {
                        MessageBox.Show("Cannot be divided by 0.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                        result = 0;
                    }
                    else
                        result /= Convert.ToDecimal(operand);
                    break;
                case "*":
                    result *= Convert.ToDecimal(operand);
                    break;

                default:
                    break;
                   
            }
        }
        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(operand) && operand.Contains(',') && operand.IndexOf(',') == (operand.Length - 1))
                operand += "0";

            if (String.IsNullOrEmpty(operand) && !String.IsNullOrEmpty(textBoxHistory.Text))
                textBoxHistory.Text = textBoxHistory.Text.Remove(lastOperationIndex, 3);

            else if (String.IsNullOrEmpty(textBoxHistory.Text) && !String.IsNullOrEmpty(operand))
                result = Convert.ToDecimal(operand);

            else if (String.IsNullOrEmpty(operand) && String.IsNullOrEmpty(textBoxHistory.Text))
            {
                textBoxHistory.Text = "0 ";
                result = 0;
            }

            if (!String.IsNullOrEmpty(textBoxHistory.Text) && !String.IsNullOrEmpty(operand) && !String.IsNullOrEmpty(operation) && !isEqual)
            {
                Calculate();

            }
     

            if (isEqual == true)
            {
                textBoxHistory.Clear();
                textBoxHistory.Text += result.ToString() + " ";
            }
            else
                textBoxHistory.Text += operand + " ";

         
                lastOperationIndex = textBoxHistory.Text.Length - 1;
                operation = ((Button)sender).Content.ToString();

          
                textBoxHistory.Text += operation + " ";
                textBoxNumber.Text = result.ToString();
            
            operand = "";
            isEqual = false;
            Clear = true;
        }

        private void buttonC_Click(object sender, RoutedEventArgs e)
        {
            textBoxHistory.Clear();
            textBoxNumber.Clear();
            Clear = true;
            operation = "";
            operand = "";
            result = 0;
            textBoxNumber.Text = result.ToString();
        }

        private void buttonEqual_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(operand) && operand.Contains(',') && operand.IndexOf(',') == (operand.Length - 1))
                operand += "0";
            if (isEqual)
            {
                textBoxHistory.Clear();
                textBoxHistory.Text += result.ToString() + " " + operation + " ";
            }
            if (String.IsNullOrEmpty(operand))
            {
                textBoxHistory.Text = textBoxHistory.Text.Remove(lastOperationIndex, 3);
                textBoxHistory.Text += " = ";
            }

            else if (String.IsNullOrEmpty(operation))
            {
                textBoxHistory.Clear();
                result = Convert.ToDecimal(operand);
                textBoxHistory.Text += result.ToString() + " = ";
            }
            else
            {
                Calculate();
                textBoxHistory.Text += operand + " = ";
            }
          
           
            textBoxNumber.Text = result.ToString();
          
            isEqual = true;
            Clear = true;
        }

        private void buttonClearLast_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(operand))
            {
                operand = operand.Remove(operand.Length - 1, 1);
                textBoxNumber.Text = operand;
                if (operand.Length == 0)
                {
                    textBoxNumber.Text = "0";
                    operand = "";
                    
                }
                
            }
        }

        private void buttonCE_Click(object sender, RoutedEventArgs e)
        {
            operand = "";         
            textBoxNumber.Text = "0";
            Clear = true;
        }

        private void buttonPoint_Click(object sender, RoutedEventArgs e)
        {
            if (!operand.Contains(",") && !String.IsNullOrEmpty(operand))
            {
                operand += ",";
                textBoxNumber.Text = operand;
            }
        }
    }
}
