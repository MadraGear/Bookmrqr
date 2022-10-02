using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmrqr.FinanceService.Data
{
    public class ProcessedBookmark
    {
        [Key]
        public string AggregateId { get; set; }

        public bool IsProcessed { get; set; }
    }
}
