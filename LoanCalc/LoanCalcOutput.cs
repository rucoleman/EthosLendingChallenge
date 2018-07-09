// <copyright file="LoanCalcOutput.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanCalc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class LoanCalcOutput
    {
        [JsonProperty(PropertyName = "monthly payment")]
        public string MonthlyPayment { get; set; }

        [JsonProperty(PropertyName = "total interest")]
        public string TotalInterest { get; set; }

        [JsonProperty(PropertyName = "total payment")]
        public string TotalPayment { get; set; }
    }
}
