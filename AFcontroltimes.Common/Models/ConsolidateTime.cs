using System;

namespace AFcontroltimes.Common.Models
{
    public class ConsolidateTime
    {
        public int EmployeeCode { get; set; }
        public DateTime ConsolidatedDate { get; set; }
        public int MinutesWorked { get; set; }
    }
}