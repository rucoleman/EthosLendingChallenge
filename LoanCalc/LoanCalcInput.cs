// <copyright file="LoanCalcInput.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanCalc
{
    /// <summary>
    /// The loan calculator "DoTheMath" method references this class in its
    /// signature.
    /// </summary>
    public class LoanCalcInput
    {
        /// <summary>
        /// Gets or sets loan amount in dollars
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets loan interest rate as a percent
        /// </summary>
        public float Interest { get; set; }

        /// <summary>
        /// Gets or sets down payment amount in dollars
        /// </summary>
        public int DownPayment { get; set; }

        /// <summary>
        /// Gets or sets duration of loan in years
        /// </summary>
        public int Term { get; set; }
    }
}
