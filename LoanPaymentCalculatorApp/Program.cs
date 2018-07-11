// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanPaymentCalculatorApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LoanCalc;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Declare string array which will be populated either with strings
            // from a file or from standard input.
            var input = null as string[];

            if (args.Length == 1)
            {
                // Populate input array from given file.
                input = File.ReadAllLines(args[0]);
            }
            else
            {
                // Populate input array from standard input.
                var lines = new List<string>();
                var line = null as string;
                while ((line = Console.ReadLine()) != null && line != string.Empty)
                {
                    lines.Add(line);
                }

                input = lines.ToArray();
            }

            // Debugging: Echo the input strings to the console.
            input.ToList().ForEach(l => Console.WriteLine(l));

            // Create a dictionary from the input lines. Each input line is
            // supposed to be of the form 'keyword: value'. The Select method
            // expands each input line into a string array of length two, where
            // the first (index=0) item is the keyword and the second (index=1)
            // item is the value. The ToDictionary method creates a dictionary
            // entry for each of the string arrays.
            var dict = input.Select(t => t.Split(':'))
                .ToDictionary(t => t[0].Trim(), t => t[1].Trim());

            // Debugging: echo the dictionary to the console.
            // Given a dictionary entry 'ent', write an interpolated string of the key and value.
            dict.ToList().ForEach(ent => Console.WriteLine($"<debug k='{ent.Key}' v='{ent.Value}'/>"));

            try
            {
                // Create loan calculator input object from the dictionary.
                var loanCalcInput = new InputConverter(dict).ToLoanCalcInput();

                // Compute and format the JSON result using the calculator input.
                var answerJSON = new LoanCalculator().DoTheMath(loanCalcInput);
                Console.WriteLine(answerJSON);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
