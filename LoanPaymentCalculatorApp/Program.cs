// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanPaymentCalculatorApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using LoanCalc;

    internal class Program
    {
        private static void ProcessFile(Func<string[]> theDel)
        {
            try
            {
                var inputs = theDel();

                // Create a dictionary from the input lines. Each input line is
                // supposed to be of the form 'keyword: value'. The Select method
                // expands each input line into a string array of length two, where
                // the first (index=0) item is the keyword and the second (index=1)
                // item is the value. The ToDictionary method creates a dictionary
                // entry for each of the string arrays.

                // Debugging: Echo the input strings to the console.
                inputs.ToList().ForEach(l => Debug.WriteLine(l));

                var dictParams = inputs
                                    .Select(t => t.Split(':'))
                                    .ToDictionary(t => t[0].Trim(), t => t[1].Trim());

                // Debugging: echo the dictionary to the console.
                // Given a dictionary entry 'ent', write an interpolated string of the key and value.
                dictParams.ToList().ForEach(ent => Debug.WriteLine($"<debug k='{ent.Key}' v='{ent.Value}'/>"));

                // Create loan calculator input object from the dictionary.
                var loanCalcInput = new InputConverter(dictParams).ToLoanCalcInput();

                // Compute and format the JSON result using the calculator input.
                var answerJSON = new LoanCalculator().DoTheMath(loanCalcInput);

                Console.WriteLine(answerJSON);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // cat TestInputFiles\input_orig.txt | LoanPaymentCalculatorApp\bin\Debug\LoanPaymentCalculatorApp.exe
                // Populate input array from standard input.
                var lines = new List<string>();
                var line = null as string;
                while ((line = Console.ReadLine()) != null && line != string.Empty)
                {
                    lines.Add(line);
                }

                Func<string[]> capture2 = () => lines.ToArray();
                ProcessFile(capture2);
            }
            else if (args.Length == 1)
            {
                // Populate input array from given file.
                var filePath = args[0];
                Func<string[]> capture = () => File.ReadAllLines(filePath);
                ProcessFile(capture);
            }
            else
            {
                // LoanPaymentCalculatorApp\bin\Debug\LoanPaymentCalculatorApp.exe  --db --file TestInputFiles\input_orig.txt
                // LoanPaymentCalculatorApp\bin\Debug\LoanPaymentCalculatorApp.exe  --db --all TestInputFiles
                for (var iA = 0; iA < args.Length; iA++)
                {
                    var arg = args[iA];

                    if (arg == "--db")
                    {
                        Debug.Listeners.Add(new ConsoleTraceListener());
                    }
                    else if (arg == "--file")
                    {
                        var file = args[iA + 1];
                        Func<string[]> capture = () => File.ReadAllLines(file);
                        ProcessFile(capture);
                    }
                    else if (arg == "--all")
                    {
                        var dirInput = args[iA + 1];
                        foreach (var path in Directory.EnumerateFiles(dirInput, "*.txt"))
                        {
                            var file = path;
                            Func<string[]> capture = () => File.ReadAllLines(file);
                            ProcessFile(capture);
                        }
                    }
                }
            }
        }
    }
}
