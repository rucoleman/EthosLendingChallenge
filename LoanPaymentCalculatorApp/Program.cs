// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanPaymentCalculatorApp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LoanCalc;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] input = null;

            if (args.Length == 1)
            {
                input = File.ReadAllLines(args[0]);
            }
            else
            {
                var lines = new List<string>();
                string line;
                while ((line = Console.ReadLine()) != null && line != string.Empty)
                {
                    lines.Add(line);
                }

                input = lines.ToArray();
            }

            input.ToList().ForEach(l => Console.WriteLine(l));

            var dict = input.Select(t => t.Split(':'))
                .ToDictionary(t => t[0].Trim(), t => t[1].Trim());

            dict.ToList().ForEach(ent => Console.WriteLine($"<debug k='{ent.Key}' v='{ent.Value}'/>"));

            try
            {
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
