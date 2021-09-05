using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AFcontroltimes.Function.Entities
{
    public class ConsolidateTimeEntity : TableEntity
    {
        public int EmployeeCode { get; set; }
        public DateTime ConsolidatedDate { get; set; }
        public int MinutesWorked { get; set; }
    }
}