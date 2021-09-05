using System;

namespace AFcontroltimes.Common.Models
{
    public class RecordTime
    {
        public int EmployeeCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}