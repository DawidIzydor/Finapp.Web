using System.Collections.Generic;
using Finapp.Dto;

namespace Finapp.Database
{
    public class FinappUser : FinappUserDto
    {
        public IEnumerable<FinancialStatus> FinancialSourceEntries { get; set; }
    }
}