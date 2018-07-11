using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoanCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalc.Tests
{
    [TestClass()]
    public class LoanCalculatorTests
    {
        private const string LeadTextParameterName = "Parameter name: ";
        private const string LeadTextActualValue = "Actual value was ";

        [TestMethod()]
        public void DoTheMathTestAmountNegative()
        {
            // Arrange
            var loanCalculator = new LoanCalculator();
            var loanCalcInput = new LoanCalcInput()
            {
                Amount = -100000,
                Interest = 0.055F,
                DownPayment = 20000,
                Term = 30
            };

            // Act
            var exceptionMessage = null as string;
            try
            {
                loanCalculator.DoTheMath(loanCalcInput);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            // Assert
            var expectedExceptionMessage = $"{LoanCalculator.NegativeNumberNotAllowedMessage}\r\n" +
                $"{LeadTextParameterName}{LoanCalculator.ParamLabelInputAmount}\r\n" +
                $"{LeadTextActualValue}{loanCalcInput.Amount}.";
            Assert.AreEqual(expectedExceptionMessage, exceptionMessage);
        }

        [TestMethod()]
        public void DoTheMathTestAmountinputDownGtrAmount()
        {
            // Arrange
            var loanCalculator = new LoanCalculator();
            var loanCalcInput = new LoanCalcInput()
            {
                Amount = 100000,
                Interest = 0.055F,
                DownPayment = 199000,
                Term = 30
            };

            // Act
            var exceptionMessage = null as string;
            try
            {
                loanCalculator.DoTheMath(loanCalcInput);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            // Assert
            var expectedExceptionMessage = $"{LoanCalculator.DownPaymentMoreThanLoanAmountMessage}\r\n" +
                $"{LeadTextParameterName}{LoanCalculator.ParamLabelInputDownPayment}\r\n" +
                $"{LeadTextActualValue}{loanCalcInput.DownPayment}.";
            Assert.AreEqual(expectedExceptionMessage, exceptionMessage);
        }

        [TestMethod()]
        public void DoTheMathTestAmountinputDownNegative()
        {
            // Arrange
            var loanCalculator = new LoanCalculator();
            var loanCalcInput = new LoanCalcInput()
            {
                Amount = 100000,
                Interest = 0.055F,
                DownPayment = -20000,
                Term = 30
            };

            // Act
            var exceptionMessage = null as string;
            try
            {
                loanCalculator.DoTheMath(loanCalcInput);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            // Assert
            var expectedExceptionMessage = $"{LoanCalculator.NegativeNumberNotAllowedMessage}\r\n" +
                $"{LeadTextParameterName}{LoanCalculator.ParamLabelInputDownPayment}\r\n" +
                $"{LeadTextActualValue}{loanCalcInput.DownPayment}.";
            Assert.AreEqual(expectedExceptionMessage, exceptionMessage);
        }

        [TestMethod()]
        public void DoTheMathTestAmountinputRateNegative()
        {
            // Arrange
            var loanCalculator = new LoanCalculator();
            var loanCalcInput = new LoanCalcInput()
            {
                Amount = 100000,
                Interest = -0.055F,
                DownPayment = 20000,
                Term = 30
            };

            // Act
            var exceptionMessage = null as string;
            try
            {
                loanCalculator.DoTheMath(loanCalcInput);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            // Assert
            var expectedExceptionMessage = $"{LoanCalculator.NegativeNumberNotAllowedMessage}\r\n" +
                $"{LeadTextParameterName}{LoanCalculator.ParamLabelInputInterest}\r\n" +
                $"{LeadTextActualValue}{loanCalcInput.Interest}.";
            Assert.AreEqual(expectedExceptionMessage, exceptionMessage);
        }

        [TestMethod()]
        public void DoTheMathTestAmountinputTermNegative()
        {
            // Arrange
            var loanCalculator = new LoanCalculator();
            var loanCalcInput = new LoanCalcInput()
            {
                Amount = 100000,
                Interest = 0.055F,
                DownPayment = 20000,
                Term = -30
            };

            // Act
            var exceptionMessage = null as string;
            try
            {
                loanCalculator.DoTheMath(loanCalcInput);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            // Assert
            var expectedExceptionMessage = $"{LoanCalculator.NegativeNumberNotAllowedMessage}\r\n" +
                $"{LeadTextParameterName}{LoanCalculator.ParamLabelInputTerm}\r\n" +
                $"{LeadTextActualValue}{loanCalcInput.Term}.";
            Assert.AreEqual(expectedExceptionMessage, exceptionMessage);
        }
    }
}