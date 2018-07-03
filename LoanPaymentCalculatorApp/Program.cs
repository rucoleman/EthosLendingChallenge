using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanCalc;

namespace LoanPaymentCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo sourceFile = new FileInfo(@"input.txt");
            TextReader sourceFileReader = new StreamReader(sourceFile.FullName);
            Console.SetIn(sourceFileReader);

            try
            {
                var dict = CreateInputDictionary.CreateDict();

                var converter = new InputConverter(dict);
                var loanCalcInput = converter.ToLoanCalcInput();

                var loanCalculator = new LoanCalculator();
                var answerJSON = loanCalculator.DoTheMath(loanCalcInput);
                Console.WriteLine(answerJSON);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
