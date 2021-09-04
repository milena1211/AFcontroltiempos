using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AFcontroltiempos.Functions.Entities
{
    public class ConsolidateTimeEntity : TableEntity
    {
        public int EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public int MinutesWorked { get; set; }
    }
}