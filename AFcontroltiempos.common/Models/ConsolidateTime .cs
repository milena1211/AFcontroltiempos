using System;

namespace AFcontroltiempos.common.Models
{
    internal class ConsolidateTime
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public int MinutesWorked { get; set; }
    }
}