using System;
using System.Collections.Generic;
using System.Text;

namespace AFcontroltimes2.Common.Models
{
    public class RecordTime
    {
        public int EmployeeCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}
