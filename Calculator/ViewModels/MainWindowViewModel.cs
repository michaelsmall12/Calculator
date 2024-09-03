using Prism.Commands;
using Prism.Mvvm;
using Serilog;
using System;

namespace Calculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Calculator";
        /// <summary>
        /// Gets or sets the application title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string sum;
        /// <summary>
        /// Gets or sets the sum
        /// </summary>
        public string Sum
        {
            get => sum;
            set=>SetProperty(ref sum, value);
        }

        private string num1;
        /// <summary>
        /// Gets or sets the first number entered
        /// </summary>
        public string Num1
        {
            get => num1;
            set => SetProperty(ref num1, value);
        }

        private string action;
        /// <summary>
        /// Gets or sets the action to perform
        /// </summary>
        public string Action
        {
            get => action;
            set => SetProperty(ref action, value);
        }

        private DelegateCommand clearCommand;
        /// <summary>
        /// Gets or sets the clear command to clear the calculator display
        /// </summary>
        public DelegateCommand ClearCommand => clearCommand?? new DelegateCommand(ClearInput);

        private DelegateCommand<object> inputNumber;
        /// <summary>
        /// Gets or sets the command to get the inputted number
        /// </summary>
        public DelegateCommand<object> InputNumber => inputNumber ?? new DelegateCommand<object>(InputNumbers);

        private DelegateCommand<object> inputAction;
        /// <summary>
        /// Gets or sets the command to set the action to be completed
        /// </summary>
        public DelegateCommand<object> InputAction => inputAction ?? new DelegateCommand<object>(InputMathmaticAction);

        private DelegateCommand calculate;
        /// <summary>
        /// Gets or sets the command to start the calulation
        /// </summary>
        public DelegateCommand Calculate => calculate ?? new DelegateCommand(CalculateSum);

        /// <summary>
        /// Calculates a sum based on the numbers inputted
        /// </summary>
        private void CalculateSum()
        {
            try
            {
                Log.Information($"New sum started, num1 = {Num1}, num2 = {Sum}, action = {Action}");
                if (!string.IsNullOrEmpty(Sum) && !string.IsNullOrEmpty(Num1))
                {
                    var firstNumber = Convert.ToDecimal(Num1);
                    var secondNumber = Convert.ToDecimal(Sum);

                    switch (action)
                    {
                        case "+":
                            Sum = (firstNumber + secondNumber).ToString();
                            break;
                        case "-":
                            Sum = (firstNumber - secondNumber).ToString();
                            break;
                        case "/":
                            Sum = (firstNumber / secondNumber).ToString();
                            break;
                        case "*":
                            Sum = (firstNumber * secondNumber).ToString();
                            break;
                        case "%":
                            Sum = ((firstNumber / secondNumber) * 100).ToString();
                            break;
                        default:
                            break;

                    }

                    Log.Information($"Result {Sum}");

                }
            }
            catch(Exception ex)
            {
                Log.Error($"Exception thrown in {nameof(CalculateSum)}");
            }           
            
        }

        /// <summary>
        /// Captures the inputted numbers
        /// </summary>
        /// <param name="parameter">The number to capture</param>
        private void InputNumbers(object parameter)
        {
            try
            {
                Log.Information($"Adding new number {parameter}");
                if (string.IsNullOrEmpty(Sum))
                {
                    Sum = parameter.ToString();
                }
                else
                {
                    Sum += parameter.ToString();
                }
            }
            catch(Exception ex)
            {
                Log.Error($"Exception thrown in {nameof(InputNumbers)}", ex);
            }
            
        }

        /// <summary>
        /// Captures the inputted action
        /// </summary>
        /// <param name="parameter">The action to capture</param>
        private void InputMathmaticAction(object parameter)
        {
            try
            {
                Log.Information($"New mathmatic action selected {parameter}");
                Num1 = Sum;
                Sum = string.Empty;
                Action = parameter.ToString();
            }
            catch(Exception ex)
            {
                Log.Error($"Exception thrown in {nameof(InputMathmaticAction)}", ex);
            }
        }

        /// <summary>
        /// Constructor for the main window view model
        /// </summary>
        public MainWindowViewModel()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/calculator.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("Creating new instance of the main window view model");
        }

        /// <summary>
        /// Clears the input field
        /// </summary>
        public void ClearInput()
        {
            Log.Information("Clearing input");
            Sum = string.Empty;
            Num1 = string.Empty;
            Action = string.Empty;
        }
    }
}
