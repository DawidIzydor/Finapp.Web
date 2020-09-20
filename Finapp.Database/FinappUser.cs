using System.Collections.Generic;

namespace Finapp.Database
{
    public class FinappUser : FinappUserDto
    {
        public IEnumerable<FinancialStatus> FinancialSourceEntries { get; set; }
    }
}