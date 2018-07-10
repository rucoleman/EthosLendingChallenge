// <copyright file="LoanCalcOutput.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanCalc
{
    using Newtonsoft.Json;

    /// <summary>
    /// The loan calculator DoTheMath method returns this class, not directly,
    /// but formatted as a JSON string. PropertyName attributes are used in
    /// order to ensure JSON object fields are named as specified in the problem
    /// statement. In the DoTheMath method, all these fields are strings
    /// formatted in dollars and cents, with a decimal point and no dollar sign.
    /// </summary>
    internal class LoanCalcOutput
    {
        [JsonProperty(PropertyName = "monthly payment")]
        public string MonthlyPayment { get; set; }

        [JsonProperty(PropertyName = "total interest")]
        public string TotalInterest { get; set; }

        [JsonProperty(PropertyName = "total payment")]
        public string TotalPayment { get; set; }
    }
}
