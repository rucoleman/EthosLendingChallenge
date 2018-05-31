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
        public class Pair
        {
            public string v1 { get; set; }
            public string v2 { get; set; }


            public Pair(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }

        static void Main(string[] args)
        {
            FileInfo sourceFile = new FileInfo(@"input.txt");
            TextReader sourceFileReader = new StreamReader(sourceFile.FullName);
            Console.SetIn(sourceFileReader);

            const int MAXLINES = 10;
            const int EXPECTEDLINECOUNT = 4;

            List<Pair> input = new List<Pair>();

            for (int i=0; i<MAXLINES; i++)
            {
                string inputstr = Console.ReadLine();
                
                if (inputstr is null || inputstr.Length == 0)
                {
                    break;
                }
                string[] inary = inputstr.Split(':');
                input.Add(new Pair(inary[0].Trim().ToLower(), inary[1].Trim().ToLower()));
            }
            int linesgiven = input.Count;
            if (linesgiven != EXPECTEDLINECOUNT)
            {
                Console.WriteLine(string.Format("Unexpected number of input lines: {0}, expecting {1}", linesgiven, EXPECTEDLINECOUNT));
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach(Pair pair in input)
            {
                dict.Add(pair.v1, pair.v2);
            }

            int parsedloanamount;
            float parsedrate;
            int parseddownpayment;
            int parsedterm;
            if (!int.TryParse(dict["amount"], out parsedloanamount))
            {
                Console.WriteLine(string.Format("loan amount given {0} is not in the correct format", dict["amount"]));
            }
            if (!float.TryParse(dict["interest"], out parsedrate))
            {
                Console.WriteLine(string.Format("interest rate given {0} is not in the correct format", dict["interest"]));
            }
            if (!int.TryParse(dict["downpayment"], out parseddownpayment))
            {
                Console.WriteLine(string.Format("down payment amount given {0} is not in the correct format", dict["downpayment"]));
            }
            if (!int.TryParse(dict["term"], out parsedterm))
            {
                Console.WriteLine(string.Format("loan term given {0} is not in the correct format", dict["term"]));
            }

            LoanCalcInput loanCalcInput = new LoanCalcInput()
            {
                amount = parsedloanamount,
                interest = parsedrate,
                downpayment = parseddownpayment,
                term = parsedterm
            };
            LoanCalculator loanCalculator = new LoanCalculator();
            string answerJSON = loanCalculator.DoTheMath(loanCalcInput);
            Console.WriteLine(answerJSON);
        }
    }
}
