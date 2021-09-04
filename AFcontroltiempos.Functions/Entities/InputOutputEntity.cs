using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AFcontroltiempos.Functions.Entities
{
    public class InputOutputEntity : TableEntity
    {
        public int EmployeeId { get; set; }
        public DateTime DateInputOrOutput { get; set; }
        public int Type { get; set; }
        public bool IsConsolidated { get; set; }
    }
}