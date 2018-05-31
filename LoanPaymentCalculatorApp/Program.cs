using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanCalc;

namespace ConsoleAppAryFromUserInput
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo sourceFile = new FileInfo(@"input.txt");
            TextReader sourceFileReader = new StreamReader(sourceFile.FullName);
            Console.SetIn(sourceFileReader);

            Dictionary<string, string> dict = CreateInputDictionary.CreateDict();

            LoanCalcInputFromDict loanCalcInputFromDict = new LoanCalcInputFromDict(dict);

            LoanCalculator loanCalculator = new LoanCalculator();
            string answerJSON = loanCalculator.DoTheMath(loanCalcInputFromDict.GetLoanCalcInput());
            Console.WriteLine(answerJSON);
        }
    }
}
