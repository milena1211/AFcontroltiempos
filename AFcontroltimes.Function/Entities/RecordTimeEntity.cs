using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AFcontroltimes.Function.Entities
{
    public class RecordTimeEntity : TableEntity
    {
        public int EmployeeCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}