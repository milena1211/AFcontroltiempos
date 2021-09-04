using System;

namespace AFcontroltiempos.common.Models
{
    internal class InputOutput
    {
        public int EmployeeId { get; set; }
        public DateTime DateInputOrOutput { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}