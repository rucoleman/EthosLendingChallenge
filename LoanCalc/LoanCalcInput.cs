// <copyright file="LoanCalcInput.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanCalc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LoanCalcInput
    {
        public int Amount { get; set; }

        public float Interest { get; set; }

        public int DownPayment { get; set; }

        public int Term { get; set; }
    }
}
