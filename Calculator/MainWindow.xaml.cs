using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private char[] operations = { '^', '√', '+', '-', '*', '/'};
        private int firstNumber = 0;
        private int secondNumber = 0;
        private char sign = ' ';
        private bool flag = false;
        
        int result = 0; 
        
        private void checkFlag()
        {
            if (flag)
            {
                TextBox.Clear();
                flag = false;
            }
            SetDefaultButtonColor();
        }
        private string convertNumbersToWords(int number)
        {
            

            string[] units = { "ноль", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
            string[] teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
            string[] tens = { "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] thousands = {"тысячи", "тысяч" , "тысяча"};
            string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };

            

            string words = "";

            if ((number / 1000) > 0)
            {
                if((number / 1000) == 1)
                {
                    words += thousands[2];
                }
                else if ((number / 1000) < 5)
                {
                    words += convertNumbersToWords(number / 1000) + " " + thousands[0];
                }
                else
                {
                    words += convertNumbersToWords(number / 1000) + " " + thousands[1];
                }
                    
                

                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += " " + hundreds[number / 100];
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += " ";
                }

                if (number < 10)
                {
                    words += units[number];
                }
                else if (number < 20)
                {
                    words += teens[number - 10];
                }
                else
                {
                    words += tens[number / 10 - 2];
                    if ((number % 10) > 0)
                    {
                        words += " " + units[number % 10];
                    }
                }
            }
            
            if (number == 0)
            {
                TextBox2.Clear();
                TextBox2.Text = "ноль";
            }
            else if (number < 0)
            {
                TextBox2.Clear();
                TextBox2.Text = "минус " + convertNumbersToWords(-number);
            }
            else
            {
                TextBox2.Clear();

                TextBox2.Text = words;
            }

            
            return words;
        }
        private void ClickNumber1(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "1";
        }

        private void ClickNumber2(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "2";
        }

        private void ClickNumber3(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "3";
        }

        private void ClickNumber4(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "4";
        }

        private void ClickNumber5(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "5";
        }

        private void ClickNumber6(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "6";
        }

        private void ClickNumber7(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "7";
        }

        private void ClickNumber8(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "8";
        }

        private void ClickNumber9(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "9";
        }

        private void ClickNumber0(object sender, RoutedEventArgs e)
        {
            checkFlag();
            TextBox.Text += "0";
        }

        private void changeChar(char charater, Button sender)
        {
            if (sign != ' ')
            {
               
                equally();
                flag = true;
            }
            else
            {  
                if (charater == '-' && TextBox.Text == "")
                    {
                        TextBox.Text += "-";
                        
                        charater = ' ';
                    }
                else
                {
                    bool success = int.TryParse(TextBox.Text, out firstNumber);
                    flag = true;
                }
            }
            sender.Background = new SolidColorBrush(Color.FromRgb(64, 224, 208));
            sign = charater;
        }

        private void ClickPow(object sender, RoutedEventArgs e)
        {
            changeChar('^', (Button)sender);
        }

        private void ClickSqrt(object sender, RoutedEventArgs e)
        {
            changeChar('√', (Button)sender);
        }

        private void ClickPlus(object sender, RoutedEventArgs e)
        {
            changeChar('+', (Button)sender);
        }

        private void ClickMinus(object sender, RoutedEventArgs e)
        {
            changeChar('-', (Button)sender);
        }

        private void ClickMultiplication(object sender, RoutedEventArgs e)
        {
            changeChar('*', (Button)sender);
        }

        private void ClickDivide(object sender, RoutedEventArgs e)
        {
            changeChar('/', (Button)sender);
        }

        private int equally()
        {
            if (!flag)
            {
                bool success = int.TryParse(TextBox.Text, out secondNumber);

                if (!success)
                {
                    MessageBox.Show("Введено не коректное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
            }
            if (secondNumber != 0 || (firstNumber < 0 && secondNumber == 0))
            {
                switch (sign)
                {
                    case '+':
                        result = firstNumber + secondNumber;
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        break;
                    case '-':
                        result = firstNumber - secondNumber;
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        
                        break;
                    case '*':
                        result = firstNumber * secondNumber;
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        break;
                    case '/':
                        result = firstNumber / secondNumber;
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        break;
                    case '√':
                        double firstResult = Math.Pow(firstNumber, (1.0 / secondNumber));
                        result = (int)firstResult;
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        break;
                    case '^':
                        result = (int)Math.Pow(firstNumber, secondNumber);
                        firstNumber = result;
                        secondNumber = 0;
                        sign = ' ';
                        break;
                }
            }
            SetDefaultButtonColor();
            convertNumbersToWords(result);
            TextBox.Clear();
            TextBox.Text = result.ToString();
            return result;
        }

        private void SetDefaultButtonColor()
        {
            foreach (Button button in FindVisualChildren<Button>(this))
            {
                button.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            List<T> children = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    children.Add((T)child);
                }
                else
                {
                    IEnumerable<T> foundChildren = FindVisualChildren<T>(child);
                    if (foundChildren != null)
                    {
                        children.AddRange(foundChildren);
                    }
                }
            }
            return children;
        }

        private void ClickEqually(object sender, RoutedEventArgs e)
        {
            equally();
        }

        private void ClickClear(object sender, RoutedEventArgs e)
        {
            TextBox.Clear();
            firstNumber = 0;
            secondNumber = 0;
            sign = ' ';
        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                TextBox.Text = TextBox.Text.Substring(0, TextBox.Text.Length - 1);
            }
        }
    }
}
