using Calculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    internal class MainWindowViewModelTests
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }

        [SetUp]
        public void Setup()
        {
            MainWindowViewModel = new MainWindowViewModel();
        }
        /// <summary>
        /// Tests the addition function
        /// </summary>
        [Test]
        public void TestAddition()
        {
            MainWindowViewModel.Num1 = "2";
            MainWindowViewModel.Sum = "2";
            MainWindowViewModel.Action = "+";
            MainWindowViewModel.Calculate.Execute();
            Assert.AreEqual("4", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests the subtraction function
        /// </summary>
        [Test]
        public void TestSubtraction()
        {
            MainWindowViewModel.Num1 = "2";
            MainWindowViewModel.Sum = "2";
            MainWindowViewModel.Action = "-";
            MainWindowViewModel.Calculate.Execute();
            Assert.AreEqual("0", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests the division function
        /// </summary>
        [Test]
        public void TestDivision()
        {
            MainWindowViewModel.Num1 = "2";
            MainWindowViewModel.Sum = "2";
            MainWindowViewModel.Action = "/";
            MainWindowViewModel.Calculate.Execute();
            Assert.AreEqual("1", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests the division function when the user tried to divide by zero
        /// </summary>
        [Test]
        public void TestDivisionByZero()
        {
            MainWindowViewModel.Num1 = "2";
            MainWindowViewModel.Sum = "0";
            MainWindowViewModel.Action = "/";
            MainWindowViewModel.Calculate.Execute();
            Assert.AreEqual("Cannot divide by zero", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests the multiplication function
        /// </summary>
        [Test]
        public void TestMultiplication()
        {
            MainWindowViewModel.Num1 = "2";
            MainWindowViewModel.Sum = "2";
            MainWindowViewModel.Action = "*";
            MainWindowViewModel.Calculate.Execute();
            Assert.AreEqual("4", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests that the user can add multiple decimal places
        /// </summary>
        [Test]
        public void TestWhenAddingANumberTheUserCantAddTwoDecimals()
        {
            MainWindowViewModel.Sum = "2.";
            MainWindowViewModel.InputNumber.Execute(".");
            Assert.AreEqual("2.", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests that a number gets appended to the active value
        /// </summary>
        [Test]
        public void TestThatAddingANumberAddsIfStringIsShorterThanTwenty()
        {
            MainWindowViewModel.Sum = "2";
            MainWindowViewModel.InputNumber.Execute("2");
            Assert.AreEqual("22", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests that addinga number fails if the total string is too long
        /// </summary>
        [Test]
        public void TestThatAddingANumberFailsIfStringIsLongerThanTwenty()
        {
            MainWindowViewModel.Sum = "222222222222222222222222222222222222222";
            MainWindowViewModel.InputNumber.Execute("2");
            Assert.AreEqual("222222222222222222222222222222222222222", MainWindowViewModel.Sum);
        }

        /// <summary>
        /// Tests that when a action is selected the active number is assigned to the Num1 value
        /// </summary>
        [Test]
        public void TestWhenExecutingAMathematicFunctionTheFirstNumberGetsAssignedToNum1()
        {
            MainWindowViewModel.Sum = "1";
            MainWindowViewModel.InputAction.Execute("+");
            Assert.AreEqual("1", MainWindowViewModel.Num1);
            Assert.AreEqual(string.Empty, MainWindowViewModel.Sum);
            Assert.AreEqual("+", MainWindowViewModel.Action);
        }
        /// <summary>
        /// Tests that the Clear command clears the input fields
        /// </summary>

        [Test]
        public void TestClearInputs()
        {
            MainWindowViewModel.ClearCommand.Execute();
            Assert.AreEqual(string.Empty, MainWindowViewModel.Num1);
            Assert.AreEqual(string.Empty, MainWindowViewModel.Sum);
            Assert.AreEqual(string.Empty, MainWindowViewModel.Action);
        }
    }
}
