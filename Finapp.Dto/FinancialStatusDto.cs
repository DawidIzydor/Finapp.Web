using System;

namespace Finapp.Dto
{
    public class FinancialStatusDto
    {
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
    }
}