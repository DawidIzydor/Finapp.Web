using System;
using System.ComponentModel.DataAnnotations;
using Finapp.Dto;

namespace Finapp.Database
{
    public class FinancialStatus : FinancialStatusDto
    {
        [Key] public Guid Id { get; set; }

        [Required] public string UserName { get; set; }

        public FinappUser User { get; set; }
    }
}