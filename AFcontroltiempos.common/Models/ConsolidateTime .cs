using System;
using System.Collections.Generic;
using System.Text;

namespace AFcontroltiempos.common.Models
{
    class ConsolidateTime
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public int MinutesWorked { get; set; }
    }
}