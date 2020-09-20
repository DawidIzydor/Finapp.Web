using System;

namespace Finapp.Dto
{
    /// <summary>
    ///     Financial status
    /// </summary>
    public class FinancialStatusDto
    {
        /// <summary>
        ///     Date of source update
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        ///     Source name
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///     Cash amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}